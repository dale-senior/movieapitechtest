using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieReadDto>> GetMovies(MovieFilterDto filters) {
            if(filters == null) {
                return BadRequest();
            }
            var movies = this._movieService.GetAllMovies(filters);
            Console.WriteLine(movies.Count());
            return Ok(movies);
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public ActionResult<MovieReadDto> GetMovieById(int id) {
            if(!this._movieService.MovieExsts(id)) {
                return NotFound();
            }
            var movie = this._movieService.GetMoveById(id);
            return Ok(movie);
        }
    }
}