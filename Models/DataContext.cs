using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVDMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace dvdmovie.Models
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> opts) : base(opts) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
