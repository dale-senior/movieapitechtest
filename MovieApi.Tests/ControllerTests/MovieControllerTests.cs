using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Controllers;
using MovieApi.Data.Repos;
using MovieApi.Dtos;
using MovieApi.Services;
using NSubstitute;
using NUnit.Framework;

namespace MovieApi.Tests.ControllerTests
{
    public class MovieControllerTests
    {
        private IMovieService service;
        private MovieController controller;

        [SetUp]
        public void Setup()
        {
            service = Substitute.For<IMovieService>();
            var dtoList = new List<MovieReadDto>();
            dtoList.Add(new MovieReadDto());
            service.GetMoveById(Arg.Any<int>()).Returns(x => new MovieReadDto());
            service.GetAllMovies(Arg.Any<MovieFilterDto>()).Returns(x => dtoList.AsEnumerable());
            controller = new MovieController(service);
        }

        [Test]
        public void Bad_Request_When_Filter_Null()
        {
            var result = this.controller.GetMovies(null);
            Console.WriteLine(result.Result);
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public void Has_Filters_Returns_200OK()
        {
            var result = this.controller.GetMovies(new MovieFilterDto());
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void Has_Filters_Returns_Dto()
        {
            var result = this.controller.GetMovies(new MovieFilterDto()).Result as OkObjectResult;
            var value = (List<MovieReadDto>) result.Value;
            Assert.That(value.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Services_Calls_Get_Movies()
        {
            var result = this.controller.GetMovies(new MovieFilterDto());
            this.service.Received().GetAllMovies(Arg.Any<MovieFilterDto>());
        }

        [Test]
        public void Not_Found_Returned_If_Movie_Not_Exists()
        {
            service.MovieExsts(Arg.Any<int>()).Returns(false);
            var result = this.controller.GetMovieById(1);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void Exists_Called()
        {
            service.MovieExsts(Arg.Any<int>()).Returns(false);
            var result = this.controller.GetMovieById(1);
            this.service.Received().MovieExsts(1);
        }

        [Test]
        public void Found_Calles_Service_Movie_By_Id()
        {
            service.MovieExsts(Arg.Any<int>()).Returns(true);
            var result = this.controller.GetMovieById(1);
            this.service.Received().GetMoveById(1);
        }

                [Test]
        public void Found_Returns_200OK()
        {
            service.MovieExsts(Arg.Any<int>()).Returns(true);
            var result = this.controller.GetMovieById(1);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

    }
}