using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Configuration
{
    public interface IAutoMapperApiService { 
        ApiMappingProfile GetMappingProfile();
    }
    public class AutoMapperApiService : IAutoMapperApiService
    {
        public ApiMappingProfile GetMappingProfile()
        {
            return new ApiMappingProfile();
        }
    }
}
