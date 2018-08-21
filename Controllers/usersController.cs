using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CoreApi.Models;
using Microsoft.AspNetCore.Http;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly userContext _context;
        public static userItem _userItem;
        public static int _permission;
        
        public UserController(userContext context)
        {
            _context = context;
        } 

        [HttpGet(Name = "GetUsers")]
        public ActionResult<List<userItem>> GetAll()
        {
            List<userItem> items = new List<userItem>();
            if( _permission == 1 )
                items = _context.users.ToList();
            else if ( _permission == 2) {
                items.Add(_userItem);
                items = items.ToList();
            }
            return items;
        }
        
        [HttpGet("logout")]
        public string GetById(long id)
        {
            _permission = 0;
            return "You logged out!";
        }      

        [HttpPost]
        public IActionResult Login(userItem user)
        {
            List<userItem> userAll = new List<userItem>();

            var result = _context.users.Where(
                                us => 
                                    us.Name == user.Name && us.Password == user.Password);
            foreach (var us in result) 
            { 
                userAll.Add(us);
            } 
            if(userAll.Count == 0) {
                return CreatedAtRoute("GetUsers", userAll);
            }
            if( userAll.First().Id == 1 ) {
                _permission = 1;
                userAll = _context.users.ToList();
                
            }
            else {
                _permission = 2;
                _userItem = userAll.First();
            }
            return CreatedAtRoute("GetUsers", userAll);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var user = _context.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}