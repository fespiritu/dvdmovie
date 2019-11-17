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
                    result.Studio.Movies = result.Studio.Movies.Select(s =>
                        new Movie
                        {
                            MovieId = s.MovieId,
                            Name = s.Name,
                            Category = s.Category,
                            Description = s.Description,
                            Price = s.Price
                        });
                }
                if(result.Ratings != null)
                {
                    result.Ratings.ForEach(r => r.Movie = null);
                }
            }

            return result;
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies(bool related = false)
        {
            IQueryable<Movie> query = context.Movies;
            if (related)
            {
                query = query.Include(m => m.Studio).Include(m => m.Ratings);
                List<Movie> data = query.ToList();
                data.ForEach(m =>
                {
                    if (m.Studio != null) {
                        m.Studio.Movies = null;
                    }

                    if (m.Ratings != null)
                    {
                        m.Ratings.ForEach(r => r.Movie = null);
                    }
                });
                return data;
            }
            return query;
        }

    }
}
