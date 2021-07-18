using System;
using System.Linq;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Users.ToArray());
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _context.Users.SingleOrDefault(x => x.Id == id.ToString());
            if (user == null)
            {
                return BadRequest();
            }

            return View("UserDetails", user);
        }
    }
}
