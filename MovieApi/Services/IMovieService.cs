using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApi.Dtos;

namespace MovieApi.Services
{
    public interface IMovieService
    {
        IEnumerable<MovieReadDto> GetAllMovies(MovieFilterDto filters);
        MovieReadDto GetMoveById(int id);
        bool MovieExsts(int id);
    }
}