using comments_api.Data;
using comments_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        private Comment? FindCommentById(int targetId, List<Comment>? replies = null)
        {
            var comments = replies ?? _context.Comments.Include(c => c.Replies).ToList();

            foreach (var comment in comments) 
                if (comment.Id == targetId) return comment;

            foreach (var comment in comments)
                if (comment.Replies.Count() > 0)
                    return FindCommentById(targetId, comment.Replies);

            return null;
        }

        //Create
        [HttpPost]
        public JsonResult Create([FromBody] Comment comment)
        {
            var itemInDb = FindCommentById(comment.Id);

            if (itemInDb != null)
            {
                return new JsonResult(BadRequest("Item already exists."));
            }

            if (comment.ParentId != null)
            {
                var parentComment = FindCommentById(comment.ParentId.Value);
                if (parentComment != null)
                {
                    parentComment.Replies.Add(comment);
                }
                else
                {       
                    return new JsonResult(BadRequest("Parent comment not found."));
                }
            }
            else
            {
                _context.Comments.Add(comment);
            }

            _context.SaveChanges();

            return new JsonResult(new
            {
                Data = comment
            });
        }

        [HttpPost]
        public JsonResult Edit([FromBody] Comment comment)
        {
            var itemInDb = FindCommentById(comment.Id);

            if (itemInDb == null)
            {
                return new JsonResult(BadRequest("Item doesn't exist."));
            }

            if (itemInDb.Content == comment.Content)
            { 
                itemInDb.Upvoted = comment.Upvoted;
                itemInDb.Score = comment.Score;
            }
            else
            {
                itemInDb.CreatedAt = DateTime.UtcNow;
                itemInDb.Content = comment.Content;
            }

            _context.SaveChanges();

            return new JsonResult(new
            {
                Data = itemInDb
            });
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var foundComment = FindCommentById(id);

            if (foundComment != null)
            {
                return new JsonResult(Ok(foundComment));
            }

            return new JsonResult(NotFound());
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var itemInDb = FindCommentById(id);

            if (itemInDb == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Comments.Remove(itemInDb);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet("/GetAll")]
        public JsonResult GetAll()
        {
            var comments = _context.Comments.ToList().Where(c=>c.ParentId==null);
            
            return new JsonResult(new
            {
                Data = comments
            });
        }


    }
}
