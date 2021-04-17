using Microsoft.EntityFrameworkCore;
using PracticeTestProjecgt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeTestProjecgt.Data
{
    public class McMovieContext : DbContext
    {
        public McMovieContext(DbContextOptions<McMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
