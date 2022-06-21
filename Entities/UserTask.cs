using System;

namespace TaskAssignment.Entities
{
    public enum Status
    {
        New, InProgress, Completed
    } 
    
    public class UserTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
        
        public int TaskGroupId { get; set;  }
        public virtual TaskGroup TaskGroup { get; set; }
    }
}