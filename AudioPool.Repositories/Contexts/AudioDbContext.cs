using Microsoft.EntityFrameworkCore;
using AudioPool.Repositories.Entities;

namespace AudioPool.Repositories.Contexts;

public class AudioDbContext : DbContext
{
    public AudioDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumArtist> AlbumArtist { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtistGenre> ArtistGenre { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlbumArtist>()
            .HasKey(aa => new { aa.AlbumsId, aa.ArtistsId });

        modelBuilder.Entity<ArtistGenre>()
            .HasKey(ag => new { ag.ArtistsId, ag.GenresId });
    }
}
