using System.Collections.Generic;
using AudioPool.Models.Dtos;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces
{
    public interface ISongService
    {
        SongDetailsDto GetSongById(int id);
        int CreateSong(SongInputModel song);
        void UpdateSong(SongInputModel song, int id);
        void DeleteSong(int id);
    }
}