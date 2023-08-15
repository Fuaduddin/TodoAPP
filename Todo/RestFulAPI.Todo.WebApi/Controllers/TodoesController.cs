using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestFulAPI.Todo.WebApi.Models;

namespace RestFulAPI.Todo.WebApi.Controllers
{
    public class TodoesController : ApiController
    {
        private TodoRestfulApiEntities3 db = new TodoRestfulApiEntities3();

        // GET: api/Todoes
        public IQueryable<RestFulAPI.Todo.WebApi.Models.Todo> GetTodoes()
        {
            return db.Todoes;
        }

        // GET: api/Todoes/5
        [ResponseType(typeof(RestFulAPI.Todo.WebApi.Models.Todo))]
        public async Task<IHttpActionResult> GetTodo(int id)
        {
            RestFulAPI.Todo.WebApi.Models.Todo todo = await db.Todoes.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        // PUT: api/Todoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTodo(int id, RestFulAPI.Todo.WebApi.Models.Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todo.TodoID)
            {
                return BadRequest();
            }

            db.Entry(todo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Todoes
        [ResponseType(typeof(RestFulAPI.Todo.WebApi.Models.Todo))]
        public async Task<IHttpActionResult> PostTodo(RestFulAPI.Todo.WebApi.Models.Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Todoes.Add(todo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = todo.TodoID }, todo);
        }

        // DELETE: api/Todoes/5
        [ResponseType(typeof(RestFulAPI.Todo.WebApi.Models.Todo))]
        public async Task<IHttpActionResult> DeleteTodo(int id)
        {
            RestFulAPI.Todo.WebApi.Models.Todo todo = await db.Todoes.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            db.Todoes.Remove(todo);
            await db.SaveChangesAsync();

            return Ok(todo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoExists(int id)
        {
            return db.Todoes.Count(e => e.TodoID == id) > 0;
        }
    }
}