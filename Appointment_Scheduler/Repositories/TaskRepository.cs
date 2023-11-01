using MongoDB.Driver;
using System.Collections.Generic;
using Appointment_Scheduler.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Appointment_Scheduler.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllAsync();
        Task<TaskModel> GetByIdAsync(BsonObjectId id);
        Task<TaskModel> InsertAsync(TaskModel model);
        Task<bool> UpdateAsync(BsonObjectId id, TaskModel model);
        Task<bool> DeleteAsync(BsonObjectId id);
    }
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<TaskModel> collection;

        string NameDataBase = "sharp";

        public TaskRepository(IMongoClient client)
        {
            var database = client.GetDatabase(NameDataBase);
            collection = database.GetCollection<TaskModel>(NameDataBase);
        }

        public async Task<List<TaskModel>> GetAllAsync()
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<TaskModel> GetByIdAsync(BsonObjectId id)
        {
         
            return await collection.Find(model => model.id == id).FirstOrDefaultAsync();
        }

        public async Task<TaskModel>InsertAsync(TaskModel model)
        {
            await collection.InsertOneAsync(model);
            return model;
        }

        public async Task<bool> UpdateAsync(BsonObjectId id, TaskModel model)
        {
            var result = await collection.ReplaceOneAsync(model => model.id == id, model);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(BsonObjectId id)
        {
            var result = await collection.DeleteOneAsync(model => model.id == id);
            return result.DeletedCount > 0;
        }
    }
}

