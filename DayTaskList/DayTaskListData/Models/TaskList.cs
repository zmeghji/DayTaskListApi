using System;
using System.Collections.Generic;
using System.Text;

namespace DayTaskListData.Models
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayOfMonth { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
