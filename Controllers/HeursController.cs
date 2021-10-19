using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeursController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ILogger<SallesController> _logger;
        public HeursController(ILogger<SallesController> logger, ApiContext context)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var users = await _context.Users
            //    .Include(u => u.Posts)
            //    .ToArrayAsync();
            var heurs = await _context.Heurs.ToArrayAsync();
            //var response = users.Select(u => new
            //{
            //    firstName = u.FirstName,
            //    lastName = u.LastName,
            //    posts = u.Posts.Select(p => p.Content)
            //});

            return Ok(heurs);
        }
    }
}
