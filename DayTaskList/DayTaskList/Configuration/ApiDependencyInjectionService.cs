using DayTaskList.ApiServices;
using DayTaskListData.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Configuration
{
    public interface IApiDependencyInjectionService
    {
        void RegisterDependencies(IServiceCollection services);
    }
    public class ApiDependencyInjectionService: IApiDependencyInjectionService
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            //repositories
            services.AddTransient<ITaskListRepository, TaskListRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();

            //api services
            services.AddTransient<ITaskListApiService, TaskListApiService>();
            services.AddTransient<ITaskApiService, TaskApiService>();
        }
        
    }
}
