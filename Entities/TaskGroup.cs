using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskAssignment.Entities
{
    public class TaskGroup 
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(5)]
        public string Name { get; set;}
        
        public virtual List<UserTask> UserTasks { get; set; }
    }
}