using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TaskAssignment.Entities
{
    public enum Status
    {
        New, InProgress, Completed
    } 
    
    public class UserTask
    {
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(5)]
        public string Name { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }

        public int TaskGroupId { get; set; }
        public virtual TaskGroup TaskGroup { get; set; }
    }
}