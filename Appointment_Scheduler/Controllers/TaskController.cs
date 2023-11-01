using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using  Appointment_Scheduler.Repositories;
using Appointment_Scheduler.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Appointment_Scheduler.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;

        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [Route("tasks")]
        public async Task<List<TaskModel>> GetAllElements()
        {
            
            var models = await _repository.GetAllAsync();
            return models;
        }

        [Route("tasks")]
        [HttpPost]
        public async Task<dynamic> Create([FromBody]TaskModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data");
            }
            var newTask = _repository.InsertAsync(model);
            return newTask;
        }
        [HttpPatch]
        [Route("tasks")]
        public async Task<TaskModel> Edit(string id, TaskModel model)
        {
            if (ModelState.IsValid)
            {
                var objectId = new BsonObjectId(id);
                var result = await _repository.UpdateAsync(objectId, model);
                if (result)
                {
                    return model;
                }
                else
                {
                    Console.WriteLine("Model == null"); 
                }
            }
            return model;
        }
        [HttpDelete]
        [Route("tasks/{id?}")]
        public async Task<dynamic> DeleteConfirmed(BsonObjectId id)
        {
            if (id.GetType() != typeof(BsonObjectId))
            {
                return BadRequest("Invalid data");
            }
            var result = await _repository.DeleteAsync(id);
            if (result == null) {
                return "error";
            }
            return "Delete работает кароч ";
        }
        [HttpGet]
        [Route("tasks/{id?}")]
        public async Task<dynamic> GetTaskById(BsonObjectId id) {
            var task = await _repository.GetByIdAsync(id);
            if (task == null) {
                return NotFound();
            }
            return task;
        }
    }   
}
