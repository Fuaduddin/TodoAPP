using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todo.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Todo.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Todo()
        {
            TodoViewModel todoitem = new TodoViewModel();
            todoitem.Todo = new TodoModel();
            todoitem.Todolist = perpageshowdata(1, 10);
            todoitem.totalpage = pagecount(10);
            return View("Todo", todoitem);
        }
        [HttpPost]
        public ActionResult AddNewTodo(TodoModel todo)
        {
            TodoViewModel todoitem = new TodoViewModel();
            if (todo.TodoID>0)
            {
                todo.TodoDateandTime = DateTime.Now;
                HttpResponseMessage responseUpdate = GlobalSettings.WebApiClient.PutAsJsonAsync("Todoes/"+ todo.TodoID.ToString(),todo).Result;
                ViewBag.responseMSG = "New Task have been Updated";
            }
            else
            {
                HttpResponseMessage responseADD = GlobalSettings.WebApiClient.PostAsJsonAsync("Todoes", todo).Result;
                ViewBag.responseMSG = "New Task have been Added";
            }
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Todoes").Result;
            todoitem.Todolist = (List<TodoModel>)response.Content.ReadAsAsync<IEnumerable<TodoModel>>().Result;
            return RedirectToAction("Todo", todoitem);
        }
        public ActionResult DeleteTodo(int id)
        {
            TodoViewModel todoitem = new TodoViewModel();
            todoitem.Todo = new TodoModel();
            HttpResponseMessage responsedelete = GlobalSettings.WebApiClient.DeleteAsync("Todoes/"+id.ToString()).Result;
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Todoes").Result;
            todoitem.Todolist = (List<TodoModel>)response.Content.ReadAsAsync<IEnumerable<TodoModel>>().Result;
            return View("Todo", todoitem);
        }
        public JsonResult GetSingleTodo(int id)
        {
            TodoModel todo = new TodoModel();
            if(id>0)
            {
                HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Todoes/" + id.ToString()).Result;
                todo = (TodoModel)response.Content.ReadAsAsync<TodoModel>().Result;
            }
            var date= todo.TodoDateandTime.Date;
            todo.TodoDateandTime = date;
            // todo.TodoDateandTime = todo.TodoDateandTime.Date;
            var result = JsonConvert.SerializeObject(todo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public int pagecount(int perpagedata)
        {
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Todoes").Result;
            List<TodoModel> Todolist = (List<TodoModel>)response.Content.ReadAsAsync<IEnumerable<TodoModel>>().Result;
            return Convert.ToInt32(Math.Ceiling(Todolist.Count() / (double)perpagedata));
        }

        public List<TodoModel> perpageshowdata(int pageindex, int pagesize)
        {
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Todoes").Result;
            List<TodoModel> Todolist = (List<TodoModel>)response.Content.ReadAsAsync<IEnumerable<TodoModel>>().Result;
            return Todolist.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
        }

        public JsonResult Getpaginatiotabledata(int pageindex, int pagesize)
        {
            List<TodoModel> Todolist = perpageshowdata(pageindex, pagesize);
            var result = JsonConvert.SerializeObject(Todolist);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchTodo(string name)
        {
            HttpResponseMessage response = GlobalSettings.WebApiClient.GetAsync("Todoes").Result;
            List<TodoModel> Todolist = (List<TodoModel>)response.Content.ReadAsAsync<IEnumerable<TodoModel>>().Result;
            var searchitem = Todolist.Where(x => x.TodoName.Contains(name));
            var result = JsonConvert.SerializeObject(searchitem);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}