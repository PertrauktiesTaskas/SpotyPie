using Database;
using Microsoft.EntityFrameworkCore;
using Models.BackEnd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spotify = Models.Spotify;

namespace Service
{
    public class Service : IDb
    {
        private readonly SpotyPieIDbContext _ctx;

        public Service(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;

            Start();
        }

        public static bool OpenFile(string path, out FileStream fs)
        {
            try
            {
                fs = File.OpenRead(path);
                return true;
            }
            catch (Exception ex)
            {
                fs = null;
                return false;
            }
        }

        public async Task<string> GetAudioPathById(int id)
        {
            try
            {
                var file = await _ctx.Items.FirstOrDefaultAsync(x => x.Id == id);
                return file.IsPlayable ? file.LocalUrl : string.Empty;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool AddAudioToLibrary(Item file)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SetAudioPlaying(int id)
        {
            try
            {
                //var audio = _ctx.NowPlaying.Add(id);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Item>> GetSongList()
        {
            try
            {
                return await _ctx.Items.Where(x => x.IsPlayable).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Start()
        {
            StartAdding();
        }

        public void StartAdding()
        {
            try
            {
                //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");
                string artist = System.IO.File.ReadAllText(@"C:\Users\Eimantas\source\repos\Models\Models\bin\Debug\netcoreapp2.1\Spotify\JSON\Artist.json");

                string albums = System.IO.File.ReadAllText(@"C:\Users\Eimantas\source\repos\Models\Models\bin\Debug\netcoreapp2.1\Spotify\JSON\Albums.json");

                var Artist = JsonConvert.DeserializeObject<Spotify.ArtistRoot>(artist);
                var Albums = JsonConvert.DeserializeObject<Spotify.AlbumRoot>(albums);

                try
                {
                    foreach (var x in Albums.Albums)
                    {
                        var model = new Models.BackEnd.Album(x);
                        for (int i = 0; i < model.Tracks.Items.Count; i++)
                        {
                            model.Tracks.Items[i].Artists = null;
                        }
                        _ctx.Albums.Add(model);
                        _ctx.SaveChanges();

                        foreach (var y in x.Tracks.Items)
                        {
                            var dbSong = _ctx.Items.Include(z => z.Artists).First(d => d.Name == y.Name);

                            if (dbSong.Artists == null)
                                dbSong.Artists = new List<Models.BackEnd.Artist>();

                            var SongArtist = Models.Helpers.GetArtist(y.Artists);
                            foreach (var z in SongArtist)
                            {
                                var dbArt = _ctx.Artists.FirstOrDefault(f => f.Name == z.Name);
                                if (dbArt == null)
                                    dbSong.Artists.Add(z);
                                else
                                {
                                    if (!dbSong.Artists.Any(yy => yy.Name == z.Name))
                                    {
                                        dbSong.Artists.Add(z);
                                    }
                                }
                            }
                            _ctx.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //private void InsertAlbums(ArtistAlbums albums)
        //{
        //    foreach (var x in albums.Items)
        //    {
        //        var a = new Models.Spotify.ArtistRoot.ArtistRoot { Artists = x.Artists;};
        //        foreach (var artist in x.Artists)
        //        {
        //            var AddToThisArtist = _ctx.Artists.FirstOrDefault(y => y.Name == artist.Name);
        //            if (AddToThisArtist != null)
        //            {

        //            }
        //        }
        //    }
        //}

        //public void InsertArtist(Spotify.ArtistRoot root)
        //{
        //    foreach (var x in root.Artists)
        //    {
        //        var model = new Models.BackEnd.Artist
        //        {
        //            Id = 0,
        //            Genres = JsonConvert.SerializeObject(x.Genres),
        //            Images = GetImages(x.Images),
        //            Name = x.Name,
        //            Popularity = 0
        //        };

        //        if (!_ctx.Artists.Any(y => y.Name.Contains(model.Name)))
        //        {
        //            _ctx.Artists.Add(model);
        //            _ctx.SaveChanges();
        //        }
        //    }
        //}

        //public List<Models.BackEnd.Image> GetImages(List<Spotify.Image> img)
        //{
        //    List<Models.BackEnd.Image> images = new List<Models.BackEnd.Image>();

        //    foreach (var x in img)
        //    {
        //        var model = new Models.BackEnd.Image
        //        {
        //            Height = x.Height,
        //            Id = 0,
        //            Url = x.Url.ToString(),
        //            Width = x.Width
        //        };
        //        images.Add(model);
        //    }
        //    return images;
        //}
    }
}
