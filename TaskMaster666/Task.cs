using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskMaster666
{
    internal class Task
    {
        public Task(string title, DateTime dueDate, string project)
        {
            Title = title;
            DueDate = dueDate;
            Project = project;
            Completed = false;
        }

        // Frågan 
        //bool outcome = DateTime.TryParse(dueDate, out DateTime validDate);
        //    if (outcome)
        //    {
        //        DueDate = validDate;
        //    }

    public Task(string title, DateTime dueDate, bool completed, string project)
        {
            Title = title;
            DueDate = dueDate;
            Completed = completed;
            Project = project;
        }

        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
        public string Project { get; set; }
    }
}
