using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Entities;
using AudioPool.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AudioPool.Repositories.Implementations
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AudioDbContext _audioDbContext;
        public ArtistRepository(AudioDbContext audioDbContext)
        {
            _audioDbContext = audioDbContext;
        }
        public IEnumerable<ArtistDto> GetAllArtists(int pageNumber, int pageSize)
        {
            var skip = pageNumber <= 1 ? 0 : (pageNumber - 1) * pageSize;
            const int bioPreviewMax = 125;

            var artists = _audioDbContext.Artists
                .OrderBy(a => a.DateOfStart)
                .Skip(skip)
                .Take(pageSize)
                .Select(a => new ArtistDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    // Í verkefna lýsingunni sýnir einungis 125 stafi af Bio, svo ég hermi bara eftir
                    Bio = a.Bio == null
                        ? ""
                        : (a.Bio.Length > bioPreviewMax
                            ? a.Bio.Substring(0, bioPreviewMax) + " [...]"
                            : a.Bio),
                    CoverImageUrl = a.CoverImageUrl ?? "",
                    DateOfStart = a.DateOfStart
                }).ToList();

            return artists;
        }
        public int GetArtistsTotalCount()
        {
            return _audioDbContext.Artists.Count();
        }
        public List<int> GetGenreIdsForArtist(int artistId)
        {
            return _audioDbContext.Artists
                .AsNoTracking()
                .Where(a => a.Id == artistId)
                .SelectMany(a => a.Genres.Select(g => g.Id))
                .ToList();
        }
        public List<int> GetArtistsByAlbumId(int albumId)
        {
            return _audioDbContext.Artists
                .AsNoTracking()
                .Where(a => a.Albums.Any(al => al.Id == albumId))
                .Select(a => a.Id)
                .ToList();
        }

        public ArtistDetailsDto GetArtistById(int id)
        {
            const int bioPreviewMax = 130;

            var artist = _audioDbContext.Artists
                .Include(a => a.Albums)
                .Include(a => a.Genres)
                .Where(a => a.Id == id)
                .Select(a => new ArtistDetailsDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Bio = a.Bio == null
                        ? ""
                        : (a.Bio.Length > bioPreviewMax
                            ? a.Bio.Substring(0, bioPreviewMax) + " [...]"
                            : a.Bio),
                    CoverImageUrl = a.CoverImageUrl ?? "",
                    DateOfStart = a.DateOfStart,
                    Albums = a.Albums.Select(al => new AlbumDto
                    {
                        Id = al.Id,
                        Name = al.Name,
                        ReleaseDate = al.ReleaseDate,
                        CoverImageUrl = al.CoverImageUrl,
                        Description = al.Description == null
                            ? ""
                            : (al.Description.Length > bioPreviewMax
                                ? al.Description.Substring(0, bioPreviewMax) + " [...]"
                                : al.Description),
                    }),
                    Genres = a.Genres.Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                }).FirstOrDefault();

            if (artist == null)
            {
                throw new KeyNotFoundException($"Artist with id {id} not found.");
            }
            return artist;
        }

        public IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId)
        {
            var albums = _audioDbContext.Albums
                .Where(al => al.Artists.Any(a => a.Id == artistId))
                .Select(al => new AlbumDto
                {
                    Id = al.Id,
                    Name = al.Name,
                    ReleaseDate = al.ReleaseDate,
                    CoverImageUrl = al.CoverImageUrl,
                    Description = al.Description
                }).ToList();
            
            return albums;
        }

        public int CreateArtist(ArtistInputModel artist)
        {
            var entity = new Artist
            {
                Name = artist.Name,
                Bio = artist.Bio,
                CoverImageUrl = artist.CoverImageUrl,
                DateOfStart = artist.DateOfStart,
                DateCreated = DateTime.UtcNow
            };

            _audioDbContext.Artists.Add(entity);
            _audioDbContext.SaveChanges();

            return entity.Id;
        }

        public void UpdateArtist(int id, ArtistInputModel artist)
        {
            var entity = _audioDbContext.Artists.FirstOrDefault(a => a.Id == id);
            if (entity == null)
            { 
                throw new KeyNotFoundException($"Artist with id {id} not found.");
            }

            entity.Name = artist.Name;
            entity.Bio = artist.Bio;
            entity.CoverImageUrl = artist.CoverImageUrl;
            entity.DateOfStart = artist.DateOfStart;
            entity.DateModified = DateTime.UtcNow;
            entity.ModifiedBy = "AudioPoolAdmin"; // Skv. verkefna skilgreiningu

            _audioDbContext.SaveChanges();
        }

        public void LinkArtistToGenre(int artistId, int genreId)
        {
            // Fetch the artist and genre entities, then add the genre to the artist's collection and add the artist to the genre's collection
            var artist = _audioDbContext.Artists
                .Include(a => a.Genres)
                .FirstOrDefault(a => a.Id == artistId);
            var genre = _audioDbContext.Genres
                .Include(g => g.Artists)
                .FirstOrDefault(g => g.Id == genreId);

            if (artist == null)
            {
                throw new KeyNotFoundException($"Artist not found (ArtistId: {artistId}.");
            }
            if (genre == null)
            { 
                throw new KeyNotFoundException($"Genre not found (GenreId: {genreId}).");
            }

            if (!artist.Genres.Contains(genre) && !genre.Artists.Contains(artist))
            {
                artist.Genres.Add(genre);
                genre.Artists.Add(artist);
            }

            _audioDbContext.SaveChanges();
        }

    }
}