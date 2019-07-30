using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using movieLibrary.CorFile;
using movieLibrary.Models;

namespace movieLibrary.Controllers
{
    public class MovieController : ApiController
    {

        [AllowCrossSite]
        [RoutePrefix("api/Movies")]
        public class MoviesController : ApiController
        {
            private ApplicationDbContext db = new ApplicationDbContext();

            // GET: api/Movies
            [Route("")]
            public IQueryable<Movie> GetMovies()
            {
                return db.movie;
            }


            //private List<Movie> movie = new List<Movie>();

            //public MovieController()
            //{
            //    movie.Add (new Movie { Id = 1, Title = "The Departed", Genre = "Drama", Director = "Martin Scorsese" });
            //    movie.Add (new Movie { Id = 2, Title = "The Dark Knight", Genre = "Drama", Director = "Christopher Nolan" });
            //    movie.Add (new Movie { Id = 3, Title = "Inception", Genre = "Drama", Director = "Christopher Nolan" });
            //    movie.Add (new Movie { Id = 4, Title = "Pineapple Express", Genre = "Comedy", Director = "David Gordon Green" });
            //    movie.Add (new Movie { Id = 5, Title = "Die Hard", Genre = "Action", Director = "John McTiernan" });
            //}


            //// GET api/<controller>
            //public IEnumerable<string> Get()
            ////public List<Movie> Get()
            //{
            //    //return movie;
            //    return new string[] { "movie1", "movie2", "movie3", "movie4", "movie5" };
            //}


            // GET: api/Movies/5
            [Route("{id:int}")]
            [HttpGet]
            [ResponseType(typeof(Movie))]
            public IHttpActionResult GetMovie(int id)
            {
                Movie movie = db.movie.Where(m => m.Id == id).SingleOrDefault();
                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(movie);
            }

            // POST: api/Movies
            [HttpPost()]
            [Route("")]
            [ResponseType(typeof(Movie))]
            public IHttpActionResult PostMovie(Movie movie)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.movie.Add(movie);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
            }


            public void Put(int id, [FromBody]string value)
            {
            }

            // DELETE api/<controller>/5
            public void Delete(int id)
            {
            }
        }
    }
}