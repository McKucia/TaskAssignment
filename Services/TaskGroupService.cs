using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskAssignment.Entities;

namespace TaskAssignment.Services
{
    public interface ITaskGroupService
    {
        TaskGroup GetById(int id); 
        IEnumerable<TaskGroup> GetAll();
        int Create(TaskGroup taskGroup);
        bool Delete(int id);
    }
    
    public class TaskGroupService : ITaskGroupService
    {
        private readonly TaskDbContext _dbContext;

        public TaskGroupService(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public TaskGroup GetById(int id)
        {
            var taskGroup = _dbContext
                .TaskGroups
                .Include(t => t.UserTasks)
                .FirstOrDefault(t => t.Id == id);
            
            return taskGroup;
        }

        public IEnumerable<TaskGroup> GetAll()
        {
            var taskGroups = _dbContext
                .TaskGroups
                .Include(t => t.UserTasks)
                .ToList();

            return taskGroups;
        }

        public int Create(TaskGroup taskGroup)
        {
            _dbContext.TaskGroups.Add(taskGroup);
            _dbContext.SaveChanges();
            
            return taskGroup.Id;
        }

        public bool Delete(int id)
        {
            var taskGroup = _dbContext
                .TaskGroups
                .FirstOrDefault(t => t.Id == id);

            if (taskGroup is null) return false;

            _dbContext.TaskGroups.Remove(taskGroup);
            _dbContext.SaveChanges();

            return true;
        }
    }
}