using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces
{
    public interface ISongRepository
    {
        SongDetailsDto GetSongById(int id);
        int CreateSong(SongInputModel song);
        void UpdateSong(int id, SongInputModel song);
        void DeleteSong(int id);
    }
}