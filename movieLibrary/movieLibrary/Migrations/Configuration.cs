namespace movieLibrary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    internal sealed class Configuration : DbMigrationsConfiguration<movieLibrary.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(movieLibrary.Models.ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            db.movie.AddOrUpdate(m => m.Id,              
                new Models.Movie { Title = "The Departed", Genre = "Drama", Director = "Martin Scorsese" },
                new Models.Movie() { Title = "The Dark Knight", Genre = "Drama", Director = "Christopher Nolan" },
                new Models.Movie { Title = "Inception", Genre = "Drama", Director = "Christopher Nolan" },
                new Models.Movie { Title = "Pineapple Express", Genre = "Comedy", Director = "David Gordon Green" },
                new Models.Movie { Title = "Die Hard", Genre = "Action", Director = "John McTiernan" }
                );

        }
    }
}
