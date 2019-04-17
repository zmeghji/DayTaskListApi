using DayTaskListData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayTaskListData.Repositories
{
    public interface ITaskRepository
    {
        bool Exists(int id);
        List<Task> Get();
        Task Get(int id);
        Task Update(Task task);
        Task Create(Task task);
        void Delete(int id);
    }
    public class TaskRepository: BaseRepository, ITaskRepository
    {
        public TaskRepository(TaskListContext taskListContext) : base(taskListContext)
        {
        }

        public bool Exists(int id)
        {
            return TaskListContext.Tasks.Any(t => t.Id == id);
        }
        public Task Get(int id)
        {
            return TaskListContext.Tasks.First(t => t.Id == id);
        }
        public List<Task> Get()
        {
            return TaskListContext.Tasks.ToListAsync().Result;
        }
        public Task Create(Task task)
        {
            var createdTask = TaskListContext.Tasks.Add(task).Entity;
            TaskListContext.SaveChanges();
            return createdTask;
        }
        public Task Update(Task task)
        {
            var updatedTask = TaskListContext.Tasks.Update(task).Entity;
            return updatedTask;
        }
        public void Delete(int id)
        {
            var task = TaskListContext.Tasks.First(t => t.Id == id);
            TaskListContext.Tasks.Remove(task);
            TaskListContext.SaveChanges();
        }

    }
}
