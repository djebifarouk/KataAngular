using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KataAPI.DbModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KataAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ILogger<ReservationController> _logger;
        public ReservationController(ILogger<ReservationController> logger, ApiContext context)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sl = await _context.Reservaion.ToArrayAsync();
            return Ok(sl);
        }
     
     
        [HttpPost]
        public async Task<ActionResult> Post(Reservaion resv)
        {
            List<Reservaion> ListReservation = new List<Reservaion>();
            var Heur = resv.idHeurReservation.Split(',').ToArray();
            foreach(string hr in Heur)
            {
                if (hr != "" && hr != null)
                {
                    if (_context.Reservaion.Where(r => r.idHeurReservation == hr && r.username == resv.username && r.dateReservation == resv.dateReservation).ToList().Count() == 0)
                    {
                        ListReservation.Add(new Reservaion
                        {
                            Id = GetUniqueKeyOriginal_BIASED(6),
                            idHeurReservation = hr,
                            idsalle = resv.idsalle,
                            dateReservation = resv.dateReservation,
                            username = resv.username

                        });
                    }
                    else
                    {


                    }
                }
            }

            _context.Reservaion.AddRange(ListReservation);
            _context.SaveChanges();
            return Ok("ok");
        }

       public static string GetUniqueKeyOriginal_BIASED(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
