﻿using Models.BackEnd;
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
        string ConvertAudio(string path, int quality);
        void RemoveCache();
        Task<string> CacheImages();
        Task<string> GetSongList();
        Task<string> GetArtistList();
        Task<List<Album>> GetAlbumsByArtist(int id);
        int GetCPUUsage();
        int GetRAMUsage();
        int GetCPUTemperature();
        Task<string> GetLibraryInfo();
        int GetUsedStorage();
        Task<long> TotalSongLength();
    }
}
