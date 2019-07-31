using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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


            // GET: api/Movies/Title
            [Route("Title/{title}")]
            public IQueryable<Movie> GetMoviesByTitle(string title)
            {
                return db.movie.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            }

            // GET: api/Movies/Genre
            [Route("Genre/{genre}")]
            public IQueryable<Movie> GetMoviesByGenre(string genre)
            {
                return db.movie.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            // GET: api/Movies/Director
            [Route("Director/{name}")]
            public IQueryable<Movie> GetMoviesByDirectorName(string name)
            {
                return db.movie.Where(b => b.Director.Equals(name, StringComparison.OrdinalIgnoreCase));
            }


            // POST: api/Movies
            [HttpPost()]
            [Route("")]
            [ResponseType(typeof(Movie))]
            public IHttpActionResult PostMovie(Movie movie, HttpPostedFileBase upload)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new Models.File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Avatar,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    movie.Files = new List<Models.File> { avatar };
                }
                db.movie.Add(movie);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
            }


            // PUT: api/Movies/5
            [HttpPut()]
            [Route("{id:int}")]
            [ResponseType(typeof(void))]
            public IHttpActionResult Put(int id, Movie movie, HttpPostedFileBase upload)
            {
                var existingMovie = db.movie.Find(id);
                if (movie.Title != null)
                {
                    existingMovie.Title = movie.Title;
                }
                if (movie.Genre != null)
                {
                    existingMovie.Genre = movie.Genre;
                }
                if (movie.Director != null)
                {
                    existingMovie.Director = movie.Director;
                }

                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (existingMovie.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(existingMovie.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new Models.File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        existingMovie.Files = new List<Models.File> { avatar };
                    }
                }


                db.SaveChanges();
                return Ok();
            }

            // DELETE: api/Movies/5
            [HttpDelete()]
            [Route("{id:int}")]
            [ResponseType(typeof(Movie))]
            public IHttpActionResult DeleteMovie(int id)
            {
                Movie movie = db.movie.Find(id);
                if (movie == null)
                {
                    return NotFound();
                }

                db.movie.Remove(movie);
                db.SaveChanges();

                return Ok(movie);
            }
        }
    }
}