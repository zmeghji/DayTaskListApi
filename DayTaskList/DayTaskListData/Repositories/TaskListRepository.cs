using DayTaskListData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayTaskListData.Repositories
{
    public interface ITaskListRepository
    {
        TaskList Get(int id);
        bool Exists(int id);
        TaskList Create(TaskList taskList);
        TaskList Update(TaskList taskList);
        void Delete(int id);
    }
    public class TaskListRepository: BaseRepository, ITaskListRepository
    {
        public TaskListRepository(TaskListContext taskListContext) : base(taskListContext)
        {
        }
        public bool Exists(int id)
        {
            return TaskListContext.TaskLists.Any(t => t.Id == id);
        }
        public TaskList Get(int id)
        {
            return TaskListContext.TaskLists.Include(t => t.Tasks).First(t => t.Id == id);
        }
        public TaskList Create(TaskList taskList)
        {
            var createdTasks = new List<Task>();
            foreach (var task in taskList.Tasks)
            {
                var createdTask = TaskListContext.Tasks.Add(task).Entity;
                createdTasks.Add(createdTask);
            }
            taskList.Tasks = createdTasks;
            var createdTaskList = TaskListContext.TaskLists.Add(taskList).Entity;
            TaskListContext.SaveChanges();
            return createdTaskList;
        }
        public TaskList Update(TaskList taskList)
        {
            var tasksFromDb = TaskListContext.Tasks.Where(t => t.TaskListId == taskList.Id);

            //are there tasks which were previously in tasklist but no longer?
            var tasksToRemove = new List<Task>();
            foreach (var taskFromDb in tasksFromDb)
            {
                if (!taskList.Tasks.Any(t => t.Id == taskFromDb.Id))
                {
                    tasksToRemove.Add(taskFromDb);
                }
            }
            TaskListContext.Tasks.RemoveRange(tasksToRemove);

            var tasks = taskList.Tasks;
            taskList.Tasks = new List<Task>();
            foreach (var task in tasks)
            {
                if (TaskListContext.Tasks.Any(t => t.Id == task.Id))
                {
                    taskList.Tasks.Add(TaskListContext.Tasks.First(t => t.Id == task.Id));
                }
                else
                {
                    taskList.Tasks.Add(TaskListContext.Tasks.Add(task).Entity);
                }
            }

            TaskListContext.Update(taskList);

            TaskListContext.SaveChanges();
            return taskList;

        }
        public void Delete(int id)
        {
            var taskList = TaskListContext.TaskLists.Where(t => t.Id == id).Include(t => t.Tasks).First();
            if (taskList.Tasks != null)
            {
                foreach (var task in taskList.Tasks)
                {
                    TaskListContext.Tasks.Remove(task);
                }
            }
            TaskListContext.Remove(taskList);
            TaskListContext.SaveChanges();

        }
    }
}
