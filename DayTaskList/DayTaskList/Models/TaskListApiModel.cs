using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Models
{
    public class TaskListApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskApiModel> Tasks { get; set; }
    }
    public class TaskApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskListId { get; set; }
    }
}
