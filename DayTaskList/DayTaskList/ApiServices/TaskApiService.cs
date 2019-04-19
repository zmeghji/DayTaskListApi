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
    public interface ITaskApiService
    {
        List<TaskApiModel> Get();
        TaskApiModel Get(int id);

        TaskApiModel Create(TaskApiModel task);

        TaskApiModel Update(TaskApiModel task);

        void Delete(int id);
    }
    public class TaskApiService : ITaskApiService
    {
        private readonly IMapper Mapper;
        private readonly ITaskRepository TaskRepository;
        public TaskApiService(IMapper mapper, ITaskRepository taskRepository)
        {
            Mapper = mapper;
            TaskRepository = taskRepository;
        }
        public List<TaskApiModel> Get()
        {
            var tasks = TaskRepository.Get();
            return Mapper.Map<List<DayTaskListData.Models.Task>, List<TaskApiModel>>(tasks);
        }

        public TaskApiModel Get(int id)
        {

            if (TaskRepository.Exists(id))
            {
                var task = TaskRepository.Get(id);
                return Mapper.Map<DayTaskListData.Models.Task, TaskApiModel>(task);
            }
            else
            {
                throw new ApiException("Task Not Found", 404);
            }
        }

        public void Delete(int id)
        {
            if (TaskRepository.Exists(id))
            {
                TaskRepository.Delete(id);
            }
            else
            {
                throw new ApiException("Task Not Found", 404);
            }
        }

        public TaskApiModel Create(TaskApiModel task)
        {
            //set to 0 in case user has attempted to explicitly set id
            task.Id = 0;

            var errors = ValidateForCreate(task);
            if (errors.Count > 0)
            {
                throw new ApiException(errors, 400);
            }
            var taskCreated = TaskRepository.Create(Mapper.Map<TaskApiModel, DayTaskListData.Models.Task>(task));
            return Mapper.Map<DayTaskListData.Models.Task, TaskApiModel>(taskCreated);
        }
        public TaskApiModel Update(TaskApiModel task)
        {
            var errors = ValidateForUpdate(task);
            if (errors.Count > 0)
            {
                throw new ApiException(errors, 400);
            }
            var taskUpdated = TaskRepository.Update(Mapper.Map<TaskApiModel, DayTaskListData.Models.Task>(task));
            return Mapper.Map<DayTaskListData.Models.Task, TaskApiModel>(taskUpdated);
        }
        private List<string> ValidateForUpdate(TaskApiModel task)
        {
            List<string> errors = new List<string>();
            if (task == null)
            {
                errors.Add("Task To Update Not Provided");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(task.Name))
            {
                errors.Add("Must specify Task Name");
            }
            if (!TaskRepository.Exists(task.Id))
            {
                errors.Add("The task does not exist");
            }
            //Make sure that task is not being switched to a different tasklist
            var taskFromDb = TaskRepository.Get(task.Id);
            if (taskFromDb.TaskListId != task.TaskListId)
            {
                errors.Add("Cannot switch task to a different tasklist");
            }
            return errors;
        }
        private List<string> ValidateForCreate(TaskApiModel task)
        {
            List<string> errors = new List<string>();

            if (task == null)
            {
                errors.Add("Task To Create Not Provided");
                return errors;
            }
            if (string.IsNullOrWhiteSpace(task.Name))
            {
                errors.Add("Must specify Task Name");
            }
            return errors;
        }
    }
}
