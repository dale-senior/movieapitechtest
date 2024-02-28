using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Repos;
using MovieApi.Dtos;
using MovieApi.Models;
using MovieApi.Services;
using NSubstitute;
using NUnit.Framework;

namespace MovieApi.Tests.ServiceTests
{
    public class MovieServiceTests
    {

        private MovieService service;
        private IMovieRepo repo;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {

            var movie = new Movie() {
                    Id = 1,
                    Genre = "",
                    OriginalLanguage = "",
                    Overview = "",
                    Popularity = 1,
                    PosterUrl = "",
                    ReleaseDate = new DateTime(),
                    Title = "",
                    VoteAverage = 1,
                    VoteCount = 1
            };
            var data = new List<Movie>();
            data.Add(movie);

            repo = Substitute.For<IMovieRepo>();
            repo.GetMovieById(Arg.Any<int>()).Returns(x => movie);
            repo.GetAllMovies().Returns(x => data.AsQueryable());
            mapper = Substitute.For<IMapper>();
            service = new MovieService(mapper, repo);
        }

        [Test]
        public void Exists_GetAllMovies_Called_On_Repo()
        {
            this.service.MovieExsts(1);
            this.repo.Received().GetAllMovies();
        }

        [Test]
        public void GetMovieById_Called_On_Repo()
        {
            this.service.GetMoveById(1);
            this.repo.Received().GetMovieById(1);
        }

        [Test]
        public void GetMovieById_Calles_Map()
        {
            this.service.GetMoveById(1);
            this.mapper.Received().Map<MovieReadDto>(Arg.Any<Movie>());
        }
    }
}