using DayTaskListData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DayTaskListData
{
    public class TaskListContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public TaskListContext(DbContextOptions<TaskListContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskList>().HasKey(t => t.Id);

            modelBuilder.Entity<Task>().HasKey(t => t.Id);

            // configure many to one for task to tasklist
            modelBuilder.Entity<Task>().
                HasOne(t => t.TaskList).
                WithMany(l => l.Tasks).
                HasForeignKey(t => t.TaskListId);
        }

    }
}
