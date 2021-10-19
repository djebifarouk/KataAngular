using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace KataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ILogger<SallesController> _logger;
        public SallesController(ILogger<SallesController> logger, ApiContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sl = await _context.Salles.ToArrayAsync();
            return Ok(sl);
        }
    }
}
