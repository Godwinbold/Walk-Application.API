using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WalkAPP.Model.Domain;

namespace WalkAPP.Data
{
    public class WalkAppDbContext : DbContext
    {
        public WalkAppDbContext(DbContextOptions dbContextOptions): base(dbContextOptions){ }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }


    }
}
