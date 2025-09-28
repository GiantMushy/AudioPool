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
using AudioPool.WebApi.Converters;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Add this line

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

builder.Services.AddControllers()
    .AddJsonOptions(o => { o.JsonSerializerOptions.Converters.Add(new TimeSpanConverter()); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with API Token support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AudioPool API", Version = "v1" });
    
    // Add API Token header configuration
    c.AddSecurityDefinition("ApiToken", new OpenApiSecurityScheme
    {
        Description = "API Token header. Example: \"AudioPoolSecretToken2024\"",
        Name = "api-token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiToken"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiToken"
                }
            },
            new string[] {}
        }
    });
});

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