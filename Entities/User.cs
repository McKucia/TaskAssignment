using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskAssignment.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string FirstName { get; set; }
        
        [Required, MaxLength(25), MinLength(5)]
        public string SecondName { get; set; }
        
    }
}