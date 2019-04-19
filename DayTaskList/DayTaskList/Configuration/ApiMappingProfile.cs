using DayTaskList.Models;
using DayTaskListData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Configuration
{
    public class ApiMappingProfile : AutoMapper.Profile
    {
        public ApiMappingProfile()
        {
            //apiModel to dto

            CreateMap<TaskListApiModel, TaskList>();
            CreateMap<TaskApiModel, DayTaskListData.Models.Task>();

            //dto to apiModel
            CreateMap<TaskList, TaskListApiModel>();
            CreateMap<DayTaskListData.Models.Task, TaskApiModel>();
        }
    }
}
