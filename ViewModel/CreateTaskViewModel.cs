using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp.ViewModel
{
    public class CreateTaskViewModel
    {
        public int taskID { get; set; }
        public string taskTitle { get; set; }
        public string taskDescription { get; set; }
        public string taskStatus { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }

    }
}