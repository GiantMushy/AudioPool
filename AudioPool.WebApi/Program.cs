using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AudioPool.Repositories.Implementations;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Implementations;
using AudioPool.Services.Interfaces;
using AudioPool.Models.Entities;
using AudioPool.Repositories.Contexts;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AudioDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("AudioDbConnectionString"), sqlLiteOptions =>
    {
        sqlLiteOptions.MigrationsAssembly("AudioPool.WebApi");
    });
});

// Add services to the container.
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<ISongService, SongService>();

// Repositories
builder.Services.AddTransient<IAlbumRepository, AlbumRepository>();
builder.Services.AddTransient<IArtistRepository, ArtistRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<ISongRepository, SongRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
