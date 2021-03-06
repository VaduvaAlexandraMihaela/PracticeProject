using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PracticeTestProjecgt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeTestProjecgt.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new McMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<McMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    },
                     new Movie
                     {
                         Title = "Extinction",
                         ReleaseDate = DateTime.Parse("2018-3-20"),
                         Genre = "Sci-fi/Thriller",
                         Price = 2.89M
                     },
                     new Movie
                     {
                         Title = "Chemical Hearts",
                         ReleaseDate = DateTime.Parse("2020-8-21"),
                         Genre = "Romance/Drama",
                         Price = 1.15M
                     }
                );
                context.SaveChanges();
            }
        }
    }
}

