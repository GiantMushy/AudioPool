using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Interfaces;

namespace AudioPool.Repositories.Implementations
{
    public class SongRepository : ISongRepository
    {
        public SongDetailsDto GetSongById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int CreateSong(SongInputModel song)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSong(int id, SongInputModel song)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSong(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}