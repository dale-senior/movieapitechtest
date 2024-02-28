using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data.Repos
{
    public class MovieRepo : IMovieRepo
    {
        private readonly AppDbContext _context;

        public MovieRepo(AppDbContext context)
        {
            this._context = context;
        }

        public void CreatePlatform(Movie Movie)
        {
            if(Movie != null) {
                this._context.Add(Movie);
            }
        }

        public IQueryable<Movie> GetAllMovies()
        {
            return this._context.Movies.AsQueryable();
        }

        public Movie GetMovieById(int id)
        {
            return this._context.Movies.FirstOrDefault(m => m.Id == id);
        }

        public bool SaveChanges()
        {
            return (this._context.SaveChanges() >= 0); 
        }
    }
}