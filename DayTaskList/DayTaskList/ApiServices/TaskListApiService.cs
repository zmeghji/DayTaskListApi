using AutoMapper;
using DayTaskList.Exceptions;
using DayTaskList.Models;
using DayTaskListData.Models;
using DayTaskListData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.ApiServices
{
    public interface ITaskListApiService
    {
        TaskListApiModel Create(TaskListApiModel taskList);
        TaskListApiModel Update(TaskListApiModel taskList);

        TaskListApiModel Get(int id);
        void Delete(int id);
    }
    public class TaskListApiService : ITaskListApiService
    {
        private readonly ITaskListRepository TaskListRepository;
        private readonly IMapper Mapper;

        public TaskListApiService(IMapper mapper, ITaskListRepository taskListRepository)
        {
            Mapper = mapper;
            TaskListRepository = taskListRepository;
        }


        public TaskListApiModel Create(TaskListApiModel taskList)
        {
            //set to 0 in case user has attempted to explicitly set id
            taskList.Id = 0;
            var errors = ValidateForCreate(taskList);
            if (errors.Count > 0)
            {
                throw new ApiException(errors, 400);
            }
            var createdTaskList = TaskListRepository.Create(Mapper.Map<TaskListApiModel, TaskList>(taskList));
            return Mapper.Map<TaskList, TaskListApiModel>(createdTaskList);
        }
        private List<string> ValidateForCreate(TaskListApiModel taskList)
        {
            List<string> errors = new List<string>();

            if (taskList == null)
            {
                errors.Add("Tasklist To Create Not Provided");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(taskList.Name))
            {
                errors.Add("Must specify Tasklist Name");
            }

            return errors;
        }

        public TaskListApiModel Update(TaskListApiModel taskList)
        {
            if (!TaskListRepository.Exists(taskList.Id))
            {
                throw new ApiException("Tasklist Not Found", 404);
            }
            //validation
            var errorMessages = ValidateForUpdate(taskList);
            if (errorMessages.Count > 0)
            {
                throw new ApiException(errorMessages, 400);
            }

            var updatedTaskList = TaskListRepository.Update(Mapper.Map<TaskListApiModel, TaskList>(taskList));
            return Mapper.Map<TaskList, TaskListApiModel>(updatedTaskList);
        }
        private List<string> ValidateForUpdate(TaskListApiModel taskList)
        {
            List<string> errors = new List<string>();

            if (taskList == null)
            {
                errors.Add("Tasklist To Update Not Provided");
                return errors;
            }
            if (!TaskListRepository.Exists(taskList.Id))
            {
                errors.Add("The Tasklist does not exist");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(taskList.Name))
            {
                errors.Add("Must specify Tasklist Name");
            }
            // make sure not updating with tasks that belong to other tasklists
            if (taskList.Tasks != null)
            {
                foreach (var task in taskList.Tasks)
                {
                    if (task.TaskListId != taskList.Id)
                    {
                        errors.Add(string.Format("The Task with id {0} belongs to a different tasklist", task.Id));
                    }
                }
            }
            return errors;
        }
        public void Delete(int id)
        {
            if (TaskListRepository.Exists(id))
            {
                TaskListRepository.Delete(id);
            }
            else
            {
                throw new ApiException("Tasklist Not Found", 404);
            }
        }

        public TaskListApiModel Get(int id)
        {
            if (TaskListRepository.Exists(id))
            {
                var taskList = TaskListRepository.Get(id);
                return Mapper.Map<TaskList, TaskListApiModel>(taskList);
            }
            else
            {
                throw new ApiException("Tasklist Not Found", 404);
            }
        }

    }
}
