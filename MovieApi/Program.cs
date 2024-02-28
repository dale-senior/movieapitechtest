using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.Data.Repos;
using MovieApi.Models;
using MovieApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Scoped
builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<IMovieService, MovieService>();

//setup context
var isProduction = builder.Environment.IsProduction();
if(isProduction) {
    Console.WriteLine("prod mode, using sql"); 
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    //didn't have time to sort this out fully so using inmem to save time
    //builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MovieDbCon")));
} else {
    Console.WriteLine("Dev mode, using in mem");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

//add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//not a production bit of code, used to speed up testing for tech test, seed the inmem db
PrepDb.PrepPopulation(app, isProduction);

app.Run();
