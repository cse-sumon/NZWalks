using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Seed data for Dificulties

            var dificulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = 1,
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = 2,
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = 3,
                    Name = "Hard",
                }
            };

            // seed dificulties to the database
            modelBuilder.Entity<Difficulty>().HasData(dificulties);


        }
    }
}
