using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Dtos;
using MovieApi.Models;

namespace MovieApi.Data.Repos
{
    public interface IMovieRepo
    {
        bool SaveChanges();
        
        IQueryable<Movie> GetAllMovies();

        Movie GetMovieById(int id);

        void CreatePlatform(Movie Movie);
    }
}