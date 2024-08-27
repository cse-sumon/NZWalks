using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : IdentityDbContext
    {
        public NZWalksDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Authentication Seeding

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "1",
                    ConcurrencyStamp = "1",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                },
                new IdentityRole
                {
                    Id = "2",
                    ConcurrencyStamp =" 2",
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id = "3",
                    ConcurrencyStamp = "3",
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);




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
