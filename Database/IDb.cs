using Models.BackEnd;
using System.IO;
using System.Threading.Tasks;

namespace Database
{
    public interface IDb
    {
        void Start();
        bool OpenFile(string path, out FileStream fs);
        Task<string> GetAudioPathById(int id);
        bool AddAudioToLibrary(Item file);
        bool SetAudioPlaying(int id);
        Task<string> GetSongList();
        Task<string> GetArtistList();

    }
}
