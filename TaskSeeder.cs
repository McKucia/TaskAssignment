using System;
using System.Collections.Generic;
using System.Linq;
using TaskAssignment.Entities;

namespace TaskAssignment
{
    public class TaskSeeder
    {
        private readonly TaskDbContext _dbContext;
        
        public TaskSeeder(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.TaskGroups.Any())
                {
                    var taskGroups = GetTaskGroups();
                    _dbContext.TaskGroups.AddRange(taskGroups);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<TaskGroup> GetTaskGroups()
        {
            var taskGroups = new List<TaskGroup>()
            {
                new TaskGroup()
                {
                    Name = "Frontend",
                    UserTasks = new List<UserTask>()
                    {
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 6, 24),
                            Name = "Finish home page",
                            Status = Status.InProgress
                        },
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 6, 25),
                            Name = "Finish contact page",
                            Status = Status.New
                        },
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 6, 27),
                            Name = "Fetch data from api",
                            Status = Status.Completed
                        },
                    }
                },
                new TaskGroup()
                {
                    Name = "Backend",
                    UserTasks = new List<UserTask>()
                    {
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 1),
                            Name = "Connect database",
                            Status = Status.New
                        },
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 2),
                            Name = "Add Authorization",
                            Status = Status.New
                        }
                    }
                },
                new TaskGroup()
                {
                    Name = "HR",
                    UserTasks = new List<UserTask>()
                    {
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 1),
                            Name = "Hire someone",
                            Status = Status.InProgress
                        },
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 2),
                            Name = "Fire someone",
                            Status = Status.InProgress
                        },
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 1),
                            Name = "Call",
                            Status = Status.Completed
                        },
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 2),
                            Name = "Talk to someone",
                            Status = Status.Completed
                        }
                    }
                },
                new TaskGroup()
                {
                    Name = "Other",
                    UserTasks = new List<UserTask>()
                    {
                        new UserTask()
                        {
                            Deadline = new DateTime(2022, 7, 1),
                            Name = "Coffe",
                            Status = Status.InProgress
                        }
                    }
                }
            };

            return taskGroups;
        }
    }
}