using System;
using System.Collections.Generic;
using System.Text;

namespace DayTaskListData.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TaskListId { get; set; }
        public TaskList TaskList { get; set; }
    }
}
