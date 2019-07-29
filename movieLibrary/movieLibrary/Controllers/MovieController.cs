using movieLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace movieLibrary.Controllers
{
    public class MovieController : ApiController
    {
        private List<Movie> movie = new List<Movie>();

        public MovieController()
        {
            movie.Add (new Movie { Id = 1, Title = "The Departed", Genre = "Drama", Director = "Martin Scorsese" });
            movie.Add (new Movie { Id = 2, Title = "The Dark Knight", Genre = "Drama", Director = "Christopher Nolan" });
            movie.Add (new Movie { Id = 3, Title = "Inception", Genre = "Drama", Director = "Christopher Nolan" });
            movie.Add (new Movie { Id = 4, Title = "Pineapple Express", Genre = "Comedy", Director = "David Gordon Green" });
            movie.Add (new Movie { Id = 5, Title = "Die Hard", Genre = "Action", Director = "John McTiernan" });
        }


        // GET api/<controller>
        //public IEnumerable<string> Get()
        public List<Movie> Get()
        {
            return movie;
            //return new string[] { "movie1", "movie2", "movie3", "movie4", "movie5" };
        }

        // GET api/<controller>/5
        public Movie Get(int id)
        {
            return movie.Where(m => m.Id == id).FirstOrDefault();
            //return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}