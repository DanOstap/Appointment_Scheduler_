using Appointment_Scheduler.Controllers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Entities;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;
using ThirdParty.Json.LitJson;
namespace Appointment_Scheduler.Models
{
 
    public class TaskModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        public string? title { get; set; }

        [Required]
        public string? description { get; set; }

        [Required]
        public string time_from { get; set; }

        [Required]
        public string time_to { get; set; }    

        [Required]
        public string  color { get; set; }

        [Required]
        public int day {get;set;}

        [Required]
        public int month{get;set;}
    }
}
