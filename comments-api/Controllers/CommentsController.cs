using comments_api.Data;
using comments_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Text.Json;

namespace comments_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApiContext _context;

        public CommentsController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public JsonResult Create([FromBody]Comment comment)
        {
            var itemInDb = _context.Comments.FirstOrDefault(c => c.Id == comment.Id);

            if (itemInDb != null) return new JsonResult(BadRequest("Item already exists."));

            if (comment.ParentId != null)
                _context.Comments.FirstOrDefault(c => c.Id == comment.ParentId).Replies.Add(comment);

            else
                _context.Comments.Add(comment);

            _context.SaveChanges();
            
            return new JsonResult(new
            {
                Data = comment
            });
        }

        [HttpPost]
        public JsonResult Edit([FromBody]Comment comment)
        {
            var itemInDb = _context.Comments.FirstOrDefault(c => c.Id == comment.Id);

            if (itemInDb == null) return new JsonResult(BadRequest("Item doesn't exist."));

            itemInDb.Content = comment.Content;
            itemInDb.CreatedAt = DateTime.Now;
            
            _context.SaveChanges();

            return new JsonResult(new
            {
                Data = itemInDb
            });
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Comments.FirstOrDefault(c => c.Id == id);

            if (result == null) return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var itemInDb = _context.Comments.FirstOrDefault(c => c.Id == id);

            if (itemInDb == null) return new JsonResult(NotFound());

            _context.Comments.Remove(itemInDb);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet("/GetAll")]
        public JsonResult GetAll() 
        {
            var result = _context.Comments.Where(c => c.ParentId == null).Include(c => c.Replies).ToList();

            return new JsonResult(new
            {
                Data = result
            });
        }
    }
}
