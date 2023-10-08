using comments_api.Data;
using comments_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace comments_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult Create([FromBody] User user)
        {
            var itemInDb = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (itemInDb != null)
                return new JsonResult(BadRequest("User already exists"));

            _context.Users.Add(user);
            _context.SaveChanges(); 

            return new JsonResult(new 
            {
                Data = user,
            });
        }

        [HttpPost]
        public JsonResult Edit(User user)
        {
            var itemInDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (itemInDb == null)
                return new JsonResult(BadRequest("User doesn't exist"));

            itemInDb.Image = user.Image;
            itemInDb.Username = user.Username;
            _context.Users.Update(itemInDb);

            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var itemInDb = _context.Users.FirstOrDefault(u => u.Id == id);

            if (itemInDb == null) return new JsonResult(BadRequest("User doesn't exist"));

            _context.Users.Remove(itemInDb);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var itemInDb = _context.Users.FirstOrDefault(u => u.Id == id);

            if (itemInDb == null) return new JsonResult(BadRequest("User doesn't exist"));

            return new JsonResult(Ok(itemInDb));
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var users = _context.Users.ToList();

            return new JsonResult(new {Data = users});
        }
    }
}
