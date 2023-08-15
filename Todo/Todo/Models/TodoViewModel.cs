using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Todo.Models
{
    public class TodoViewModel
    {
        public List<TodoModel> Todolist { get; set; }
        public TodoModel Todo { get; set; }
        public int totalpage { get; set; }
    }
}