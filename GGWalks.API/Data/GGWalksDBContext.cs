using GGWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace GGWalks.API.Data
{
    public class GGWalksDBContext:DbContext
    {
        public GGWalksDBContext(DbContextOptions<GGWalksDBContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        //Db collections(tables) of all entities
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("f11e1ef8-bf11-4b54-b083-66d9c7ed096a") ,
                    Name ="Easy"
                },
                new Difficulty
                {
                    Id =  Guid.Parse("9ae1daa7-4787-408e-9f80-eba016fced94"),
                    Name ="Medium"
                },
                new Difficulty
                {
                    Id =  Guid.Parse("5c0fa415-0c68-43f2-8ad6-32388a9ca610"),
                    Name ="Hard"
                },
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("4e87bdce-b9a7-4506-aaf2-15dc1e892cfd"),
                    Code = "CC",
                    Name = "Cyber City",
                    RegionImageUrl = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/0f/b8/e2/a4/photo5jpg.jpg?w=1600&h=-1&s=1"
                },
                new Region
                {
                    Id = Guid.Parse("2539941c-7c1e-4110-93e3-486dac8394b7"),
                    Code = "UV",
                    Name = "Udyog Vihar",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("52f96bc5-87e0-44c2-bc09-fb704b97292b"),
                    Code = "PV",
                    Name = "Palam vihar",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("38f20107-91b9-42ec-a49a-7d46b39f693c"),
                    Code = "CH",
                    Name = "Cyber Hub",
                    RegionImageUrl = "https://b.zmtcdn.com/data/pictures/2/18289242/2fd5c943d0a52b9ebcbb50c674b8ca8c.jpg?w=1600&h=-1&s=1"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
