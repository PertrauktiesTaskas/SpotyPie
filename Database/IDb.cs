using Models.BackEnd;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Database
{
    public interface IDb
    {
        void Start();
        bool OpenFile(string path, out FileStream fs);
        Task<string> GetAudioPathById(int id);
        Task<bool> AddAudioToLibrary(string path, string name, Item file);
        bool SetAudioPlaying(int id);
        void RemoveCache();
        Task<string> CacheImages();
        Task<string> GetSongList();
        Task<string> GetArtistList();
        Task<List<Album>> GetAlbumsByArtist(int id);

    }
}
