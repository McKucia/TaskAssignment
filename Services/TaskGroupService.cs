using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskAssignment.Entities;

namespace TaskAssignment.Services
{
    public interface ITaskGroupService
    {
        TaskGroup GetById(int id); 
        IEnumerable<TaskGroup> GetAll(string orderBy);
        int Create(TaskGroup taskGroup);
        bool Delete(int id);
        bool DeleteTask(int id);
        bool CreateTask(UserTask task, int id);
        bool ChangeName(int id, string name);
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

        public IEnumerable<TaskGroup> GetAll(string orderBy)
        {
            var taskGroups = _dbContext
                .TaskGroups
                .Include(t => t.UserTasks)
                .ToList();

            switch (orderBy)
            {
                case "id":
                    taskGroups = taskGroups.OrderBy(t => t.Id).ToList();
                    break;
                case "name":
                    taskGroups = taskGroups.OrderBy(t => t.Name).ToList();
                    break;
                case "tasks":
                    taskGroups = taskGroups.OrderByDescending(t => t.UserTasks.Count).ToList();
                    break;
                default:
                    taskGroups = taskGroups.OrderBy(t => t.Id).ToList();
                    break;
            }

            return taskGroups;
        }

        public int Create(TaskGroup taskGroup)
        {
            _dbContext.TaskGroups.Add(taskGroup);
            _dbContext.SaveChanges();
            
            return taskGroup.Id;
        }
        
        public bool CreateTask(UserTask task, int id)
        {
            var taskGroup = _dbContext
                .TaskGroups
                .FirstOrDefault(t => t.Id == id);

            if (taskGroup is null) return false;
            
            task.Status = Status.New;
            task.TaskGroup = taskGroup;
            
            _dbContext.UserTasks.Add(task);
            _dbContext.SaveChanges();

            return true;
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
        
        public bool DeleteTask(int id)
        {
            var task = _dbContext
                .UserTasks
                .FirstOrDefault(t => t.Id == id);

            if (task is null) return false;

            _dbContext.UserTasks.Remove(task);
            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeName(int id, string name)
        {
            var taskGroup = _dbContext
                .TaskGroups
                .FirstOrDefault(t => t.Id == id);

            if (taskGroup is null) return false;

            taskGroup.Name = name;
            _dbContext.Entry(taskGroup).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return true;
        }
    }
}