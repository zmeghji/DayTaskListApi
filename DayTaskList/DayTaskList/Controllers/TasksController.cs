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
    public interface ITasksController
    {
        ActionResult Get();
        ActionResult Get(int id);
        ActionResult Post(TaskApiModel taskApiModel);
        ActionResult Put(TaskApiModel taskApiModel);
        ActionResult Delete(int id);
    }
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase , ITasksController
    {
        private readonly ITaskApiService TaskApiService;
        private readonly ILogger<TasksController> Logger;

        public TasksController(ILogger<TasksController> logger, ITaskApiService taskApiService)
        {
            Logger = logger;
            TaskApiService = taskApiService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(TaskApiService.Get());
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            return Ok(TaskApiService.Get(id));
        }

        [HttpPost]
        public ActionResult Post(TaskApiModel taskApiModel)
        {
            return Ok(TaskApiService.Create(taskApiModel));
        }

        [HttpPut]
        public ActionResult Put(TaskApiModel taskApiModel)
        {
            return Ok(TaskApiService.Update(taskApiModel));
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            TaskApiService.Delete(id);
            return Ok();
        }
    }
}