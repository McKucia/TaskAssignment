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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTask>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(25);
        }

    }
}