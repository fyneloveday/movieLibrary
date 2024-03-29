﻿using System;
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
                return db.movie.Include(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            }

            // GET: api/Movies/Genre
            [Route("Genre/{genre}")]
            public IQueryable<Movie> GetMoviesByGenre(string genre)
            {
                return db.movie.Include(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            // GET: api/Movies/Director
            [Route("Director/{name}")]
            public IQueryable<Movie> GetMoviesByDirectorName(string name)
            {
                return db.movie.Include(b => b.Director.Equals(name, StringComparison.OrdinalIgnoreCase));
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
            public IHttpActionResult Put(Movie movie, HttpPostedFileBase upload)
            {
                //var existingMovie = db.movie.Find(id);
                db.Entry(movie).State = EntityState.Modified;

                if (upload != null && upload.ContentLength > 0)
                {
                    if (movie.Files.Any(f => f.FileType == FileType.Avatar))
                    {
                        db.Files.Remove(movie.Files.First(f => f.FileType == FileType.Avatar));
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
                    movie.Files = new List<Models.File> { avatar };
                }


                if (movie.Title != null)
                {
                    movie.Title = movie.Title;
                }
                if (movie.Genre != null)
                {
                    movie.Genre = movie.Genre;
                }
                if (movie.Director != null)
                {
                    movie.Director = movie.Director;
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