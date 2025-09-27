using AudioPool.Models;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public PageDto<ArtistDto> GetAllArtists(int pageNumber, int pageSize)
        {
            var artists = _artistRepository.GetAllArtists(pageNumber, pageSize).ToList();
            var totalCount = _artistRepository.GetArtistsTotalCount();
            var maxPages = (int)Math.Ceiling((double)totalCount / pageSize);

            // Build HATEOAS links
            foreach (var artist in artists)
            {
                artist.Links.AddReference("self", $"api/artists/{artist.Id}");
                artist.Links.AddReference("edit", $"api/artists/{artist.Id}");
                artist.Links.AddReference("delete", $"api/artists/{artist.Id}");
                artist.Links.AddReference("albums", $"api/artists/{artist.Id}/albums");

                var genreIds = _artistRepository.GetGenreIdsForArtist(artist.Id);
                artist.Links.AddListReference("genres", genreIds.Select(gid => $"api/genres/{gid}"));
            }

            var page = new PageDto<ArtistDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                MaxPages = maxPages,
                Items = artists
            };

            return page;
        }

        public ArtistDetailsDto GetArtistById(int id)
        {
            var artist = _artistRepository.GetArtistById(id);

            artist.Links.AddReference("self", $"api/artists/{id}");
            artist.Links.AddReference("edit", $"api/artists/{id}");
            artist.Links.AddReference("delete", $"api/artists/{id}");
            artist.Links.AddReference("albums", $"api/artists/{id}/albums");

            var genreIds = _artistRepository.GetGenreIdsForArtist(id);
            artist.Links.AddListReference("genres", genreIds.Select(gid => $"api/genres/{gid}"));

            return artist;
        }
        public IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId)
        {
            var album = _artistRepository.GetAlbumsByArtistId(artistId);

            foreach (var al in album)
            {
                al.Links.AddReference("self", $"api/albums/{al.Id}");
                al.Links.AddReference("delete", $"api/albums/{al.Id}");
                al.Links.AddReference("songs", $"api/albums/{al.Id}/songs");
                var artistIds = _artistRepository.GetArtistsByAlbumId(artistId).Select(a => artistId);
                al.Links.AddListReference("artists", artistIds.Select(aid => $"api/artists/{aid}"));
            }

            return album;
        }

        public int CreateArtist(ArtistInputModel artist)
        {
            return _artistRepository.CreateArtist(artist);
        }

        public void UpdateArtist(ArtistInputModel artist, int id)
        {
            _artistRepository.UpdateArtist(id, artist);
        }

        public void LinkArtistToGenre(int artistId, int genreId)
        {
            _artistRepository.LinkArtistToGenre(artistId, genreId);
        }
    }
}