using System.Collections.Generic;
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

        public IEnumerable<ArtistDto> GetAllArtists()
        {
            return _artistRepository.GetAllArtists();
        }

        public ArtistDetailsDto GetArtistById(int id)
        {
            return _artistRepository.GetArtistById(id);
        }
        public IEnumerable<AlbumDto> GetAlbumsByArtistId(int artistId)
        {
            return _artistRepository.GetAlbumsByArtistId(artistId);
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