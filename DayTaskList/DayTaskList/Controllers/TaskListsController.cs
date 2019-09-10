using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DayTaskList.ApiServices;
using DayTaskList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DayTaskList.Controllers
{
    public interface ITaskListsController
    {
        ActionResult Get(int id);
        ActionResult Post(TaskListApiModel taskListApiModel);
        ActionResult Put(TaskListApiModel taskListApiModel);
        ActionResult Delete(int id);
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase , ITaskListsController
    {
        private readonly ITaskListApiService TaskListApiService;
        private readonly ILogger<TaskListsController> Logger;

        public TaskListsController(ILogger<TaskListsController> logger, ITaskListApiService taskListApiService)
        {
            Logger = logger;
            TaskListApiService = taskListApiService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id of the task list to return</param>
        /// <returns>A Task List Object</returns>
        [Route("{id}")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(TaskListApiService.Get(id));
        }


        [HttpPost]
        public ActionResult Post(TaskListApiModel taskListApiModel)
        {
            var responseModel = TaskListApiService.Create(taskListApiModel);
            return Ok(responseModel);
        }
        [HttpPut]
        public ActionResult Put(TaskListApiModel taskListApiModel)
        {
            var responseModel = TaskListApiService.Update(taskListApiModel);
            return Ok(responseModel);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            TaskListApiService.Delete(id);
            return Ok();
        }

    }
}