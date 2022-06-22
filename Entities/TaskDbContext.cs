using Microsoft.EntityFrameworkCore;

namespace TaskAssignment.Entities
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}