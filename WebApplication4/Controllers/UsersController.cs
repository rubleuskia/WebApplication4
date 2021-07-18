using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
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
        public IActionResult Details([Range(1, int.MaxValue)]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return BadRequest();
            }

            return View("UserDetails", user);
        }
    }
}
