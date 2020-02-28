using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using asp.net_core_web_api_swagger_swashbuckle.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp.net_core_web_api_swagger_swashbuckle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly List<Movie> _moviesRepository;

        public MoviesController()
        {
            _moviesRepository = new List<Movie>
            {
                new Movie { Id = 1, Name = "Avengers", ReleaseDate = new DateTime(2012, 1, 1), Director = "Joss Whedon" },
                new Movie { Id = 2, Name = "Avengers: Age of Ultron", ReleaseDate = new DateTime(2015, 1, 1), Director = "Joss Whedon" },
                new Movie { Id = 3, Name = "Avengers: Infinity War", ReleaseDate = new DateTime(2018, 1, 1), Director = "Anthony Russo and Joe Russo" },
                new Movie { Id = 4, Name = "Avengers: Endgame", ReleaseDate = new DateTime(2019, 1, 1), Director = "Anthony Russo and Joe Russo" },
            };
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Movie>), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            try
            {
                return new OkObjectResult(_moviesRepository);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{movieId:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        public IActionResult GetMovie(int movieId)
        {
            if (movieId <= 0 || movieId > 4)
                return NotFound();

            try
            {
                var movie = _moviesRepository.Where(m => m.Id == movieId).First();
                return new OkObjectResult(movie);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}