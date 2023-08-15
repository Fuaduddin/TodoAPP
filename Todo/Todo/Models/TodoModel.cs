using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class TodoModel
    {
        public int TodoID { get; set; }
        public string TodoName { get; set; }
        public string TodoDetails { get; set; }
        public DateTime TodoDateandTime { get; set; }
        public int TodoUpdate { get; set; }
    }
}