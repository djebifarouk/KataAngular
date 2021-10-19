using Microsoft.EntityFrameworkCore;
using KataAPI.DbModels;

namespace KataAPI
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        
        public DbSet<DbModels.Salle> Salles { get; set; }

        public DbSet<Reservaion> Reservaion { get; set; }

        public DbSet<DbModels.Heur> Heurs { get; set; }
    }
}
