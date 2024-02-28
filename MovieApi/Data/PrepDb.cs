using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Dtos;
using MovieApi.Models;
// using PlatformService.Models;

namespace MovieApi.Data
{
    public static class PrepDb
    {
        /*
            this is not production code, 
            its simply for ease and quickness for the test

        */
        private static bool _isProduction = false;

        public static void PrepPopulation(WebApplication app, bool isProduction) {
            _isProduction = isProduction;

            using(var serviceScope = app.Services.CreateScope()) {
                seeding(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void seeding(AppDbContext context) {
            if(_isProduction) {
                Console.WriteLine("applying migrations");
                try {
                    //context.Database.Migrate();
                    //didn't have time to implemnt so using inmem to save time
                }
                catch(Exception ex) {
                    Console.WriteLine($"error in migrations: {ex.Message}");
                }
            }
            
            if(!context.Movies.Any()) {
                var csvconfig = new CsvConfiguration(CultureInfo.InvariantCulture){
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                using(var reader = new StreamReader("./Resources/moviedata.csv"))
                using(var csvReader = new CsvReader(reader, csvconfig)) {
                    var csvmoviedto = csvReader.GetRecords<MovieCsvDto>();
                    foreach(MovieCsvDto dto in csvmoviedto) {
                        context.Add(new Movie() {
                            Genre = dto.Genre,
                            OriginalLanguage = dto.OriginalLanguage,
                            Overview = dto.Overview,
                            Popularity = dto.Popularity.IsNullOrEmpty() ? 0 : decimal.Parse(dto.Popularity),
                            PosterUrl = dto.PosterUrl,
                            ReleaseDate = dto.ReleaseDate,
                            Title = dto.Title,
                            VoteAverage = dto.VoteAverage.IsNullOrEmpty() ? 0 : decimal.Parse(dto.VoteAverage),
                            VoteCount = dto.VoteCount.IsNullOrEmpty() ? 0 : int.Parse(dto.VoteCount)
                        });    
                    }
                    
                    context.SaveChanges();
                }
           }
        }
    }
}