using System;
using System.Collections.Generic;
using System.Text;

namespace DayTaskListData.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly TaskListContext TaskListContext;
        public BaseRepository(TaskListContext taskListContext)
        {
            TaskListContext = taskListContext;
        }
    }
}
