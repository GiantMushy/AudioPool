using Microsoft.EntityFrameworkCore;
using AudioPool.Repositories.Entities;

namespace AudioPool.Repositories.Contexts;

public class AudioDbContext : DbContext
{
    public AudioDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }
}
