using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dvdmovie.Models;
using DVDMovie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dvdmovie.Controllers
{
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private DataContext context;
        public MovieController(DataContext ctx)
        {
            context = ctx;
        }

        [HttpGet("{id}")]
        public Movie GetMovie(long id)
        {
            Movie result = context.Movies
                .Include(m => m.Studio).ThenInclude(s => s.Movies)
                .Include(m => m.Ratings)
                .FirstOrDefault(m => m.MovieId == id);

            if (result != null)
            {
                if (result.Studio !=null)
                {
                    result.Studio.Movies = null;
                }
                if(result.Ratings != null)
                {
                    result.Ratings.ForEach(r => r.Movie = null);
                }
            }

            return result;
        }

        [HttpGet]
        public List<Movie> GetMovie()
        {
            return context.Movies.ToList();
        }
       
    }
}
