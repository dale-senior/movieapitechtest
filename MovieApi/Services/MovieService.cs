using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieApi.Data.Repos;
using MovieApi.Dtos;
using MovieApi.Models;
using System.Linq.Dynamic.Core;
using Microsoft.IdentityModel.Tokens;

namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepo _movieRepo;

        public MovieService(IMapper mapper, IMovieRepo movieRepo)
        {
            this._mapper = mapper;
            this._movieRepo = movieRepo;
        }

        public IEnumerable<MovieReadDto> GetAllMovies(MovieFilterDto filters)
        {
            IQueryable<Movie> movies = this._movieRepo.GetAllMovies().OrderBy(filters.OrderBy!, filters.IsDescending);

            if(!filters.NameQuery.IsNullOrEmpty()) {
                movies = movies.Where(w => w.Title.ToUpper().Contains(filters.NameQuery!.ToUpper()));
            }

            if(!filters.GenreQuery.IsNullOrEmpty()) {
                movies = movies.Where(w => w.Genre.ToUpper().Contains(filters.GenreQuery!.ToUpper()));
            }

            movies = movies.Skip((filters.PageNumber - 1) * filters.PageSize)
                           .Take(filters.PageSize);

            var moviesDto = this._mapper.Map<IEnumerable<MovieReadDto>>(movies);
            return moviesDto;
        }

        public MovieReadDto GetMoveById(int id)
        {
            var movie = this._movieRepo.GetMovieById(id);
            return this._mapper.Map<MovieReadDto>(movie);
        }

        public bool MovieExsts(int id)
        {
            return this._movieRepo.GetAllMovies().Any(a => a.Id == id);
        }
    }
}