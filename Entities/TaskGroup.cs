using System.Collections.Generic;

namespace TaskAssignment.Entities
{
    public class TaskGroup 
    {
        public int Id { get; set; }
        public string Name { get; set;}
        
        public virtual List<UserTask> UserTasks { get; set; }
    }
}