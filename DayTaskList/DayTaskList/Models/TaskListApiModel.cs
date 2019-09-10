using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DayTaskList.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskListApiModel
    {
        /// <summary>
        /// Represents Unique ID of the Task List
        /// </summary>
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
