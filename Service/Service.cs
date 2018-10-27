using Database;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.BackEnd;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spotify = Models.Spotify;

namespace Services
{
    public class Service : IDb
    {
        private readonly SpotyPieIDbContext _ctx;

        public Service(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;
            //Start();
        }

        public bool OpenFile(string path, out FileStream fs)
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

        public async Task<string> GetSongList()
        {
            try
            {
                var list = await _ctx.Items
                    .Where(x => x.IsPlayable)
                    .Select(x =>
                    new
                    {
                        Artist = JsonConvert.DeserializeObject<List<Artist>>(x.Artists)[0].Name,
                        x.DurationMs,
                        x.IsPlayable,
                        x.Name
                    })
                    .ToListAsync();

                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetArtistList()
        {
            try
            {
                var list = await _ctx.Artists
                    .Select(x =>
                    new
                    {
                        x.Name
                    })
                    .ToListAsync();

                return JsonConvert.SerializeObject(list);
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
                string artist = System.IO.File.ReadAllText(@"C:\Users\Eimantas\source\repos\Models\Models\bin\Debug\netcoreapp2.1\Spotify\JSON\Artist19data.json");

                string albums = System.IO.File.ReadAllText(@"C:\Users\Eimantas\source\repos\Models\Models\bin\Debug\netcoreapp2.1\Spotify\JSON\14.json");

                var Artist = JsonConvert.DeserializeObject<Spotify.ArtistRoot>(artist);
                var Albums = JsonConvert.DeserializeObject<Spotify.AlbumRoot>(albums);

                InsertArtist(Albums);
                UpdateArtisthFullData(Artist);
                InsertCopyrights(Albums);
                InsertAlbums(Albums);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void UpdateArtisthFullData(Spotify.ArtistRoot artist)
        {
            foreach (var art in artist.Artists)
            {
                if (art == null)
                    continue;

                var DbArt = _ctx.Artists.Include(x => x.Images).FirstOrDefault(x => x.Name == art.Name);
                if (DbArt != null)
                {
                    DbArt.Genres = JsonConvert.SerializeObject(art.Genres.ToList());
                    if (DbArt.Images == null)
                        DbArt.Images = new List<Image>();

                    if (DbArt.Images.Count == 0)
                        DbArt.Images.AddRange(Helpers.GetImages(art.Images));

                    _ctx.Entry(DbArt).State = EntityState.Modified;
                    _ctx.SaveChanges();
                }
            }
        }

        public void InsertArtist(Spotify.AlbumRoot album)
        {
            List<Models.BackEnd.Artist> DistintArtist = _ctx.Artists.ToList();

            if (DistintArtist == null)
                DistintArtist = new List<Models.BackEnd.Artist>();

            foreach (var x in album.Albums)
            {
                foreach (var a in Helpers.GetArtist(x.Artists))
                {
                    if (!DistintArtist.Any(z => z.Name == a.Name))
                    {
                        DistintArtist.Add(a);
                        _ctx.Artists.Add(a);
                    }
                }
            }
            _ctx.SaveChanges();
        }

        public void InsertCopyrights(Spotify.AlbumRoot album)
        {
            List<Models.BackEnd.Copyright> DistintArtist = _ctx.Copyrights.ToList();

            if (DistintArtist == null)
                DistintArtist = new List<Models.BackEnd.Copyright>();

            foreach (var x in album.Albums)
            {
                foreach (var a in Helpers.GetCopyrights(x.Copyrights))
                {
                    if (!DistintArtist.Any(z => z.Text == a.Text && z.Type == a.Type))
                    {
                        DistintArtist.Add(a);
                        _ctx.Copyrights.Add(a);
                    }
                }
            }
            _ctx.SaveChanges();
        }

        public void InsertAlbums(Spotify.AlbumRoot album)
        {
            List<Models.BackEnd.Artist> DistintArtist = _ctx.Artists.ToList();
            List<Models.BackEnd.Copyright> DistintCopyrights = _ctx.Copyrights.ToList();

            List<Models.BackEnd.Album> DistintAlbum = _ctx.Albums.ToList();

            if (DistintAlbum == null)
                DistintAlbum = new List<Models.BackEnd.Album>();

            foreach (var x in album.Albums)
            {
                var albumGood = new Models.BackEnd.Album(x);
                albumGood.Songs = null;

                if (!DistintAlbum.Any(z => z.Name == x.Name))
                {
                    DistintAlbum.Add(albumGood);

                    _ctx.Albums.Add(albumGood);
                    _ctx.SaveChanges();
                }
                BindArtistToAlbum(x);
                BindCoryrightsToAlbum(x);
                InsertTracks(x);

            }
            _ctx.SaveChanges();
        }

        private void InsertTracks(Spotify.Album x)
        {
            //Einu per albumo dainas
            foreach (var song in Helpers.GetItems(x.Tracks.Items))
            {
                //Albumas su dainomis
                var dbSong = _ctx.Albums.Include(y => y.Songs)
                    .FirstOrDefault(ar => ar.Name == x.Name);

                if (dbSong != null)
                {
                    if (dbSong.Songs == null)
                        dbSong.Songs = new List<Models.BackEnd.Item>();

                    if (!dbSong.Songs.Any(y => y.Name == song.Name))
                    {
                        //ADDING SONG TO ALBUM
                        song.Artists = JsonConvert.SerializeObject(Helpers.GetArtist(x.Artists));
                        dbSong.Songs.Add(song);
                        _ctx.Entry(dbSong).State = EntityState.Modified;
                        _ctx.SaveChanges();
                    }
                    addSongToArtist(song);
                }
            }

            void addSongToArtist(Models.BackEnd.Item song)
            {
                //ADDING SONG TO ARTIST
                foreach (var AlbumArtist in x.Artists)
                {
                    var artist = _ctx.Artists.Include(y => y.Songs).FirstOrDefault(y => y.Name == AlbumArtist.Name);
                    if (artist != null)
                    {
                        if (artist.Songs == null)
                            artist.Songs = new List<Models.BackEnd.Item>();
                        if (!artist.Songs.Any(y => y.Name == song.Name))
                        {
                            artist.Songs.Add(_ctx.Items.First(y => y.Name == song.Name));
                            _ctx.Entry(artist).State = EntityState.Modified;
                            _ctx.SaveChanges();
                        }
                    }
                }
            }
        }

        public void BindArtistToAlbum(Spotify.Album x)
        {
            foreach (var artist in Helpers.GetArtist(x.Artists))
            {
                var albumartist = _ctx.Artists.Include(y => y.Albums)
                    .FirstOrDefault(ar => ar.Name == artist.Name);

                if (albumartist != null)
                {
                    if (albumartist.Albums == null)
                        albumartist.Albums = new List<Models.BackEnd.Album>();

                    if (!albumartist.Albums.Any(y => y.Name == x.Name))
                    {
                        albumartist.Albums.Add(_ctx.Albums.First(xx => xx.Name == x.Name));
                        _ctx.Entry(albumartist).State = EntityState.Modified;
                        _ctx.SaveChanges();
                    }
                }
            }
        }

        public void BindCoryrightsToAlbum(Spotify.Album x)
        {
            foreach (var copyrigth in Helpers.GetCopyrights(x.Copyrights))
            {
                var AlbumCopyRight = _ctx.Copyrights.Include(y => y.Albums)
                    .FirstOrDefault(ar => ar.Text == copyrigth.Text && ar.Type == copyrigth.Type);

                if (AlbumCopyRight != null)
                {
                    if (AlbumCopyRight.Albums == null)
                        AlbumCopyRight.Albums = new List<Models.BackEnd.Album>();

                    if (!AlbumCopyRight.Albums.Any(y => y.Name == x.Name))
                    {
                        AlbumCopyRight.Albums.Add(_ctx.Albums.First(xx => xx.Name == x.Name));
                        _ctx.Entry(AlbumCopyRight).State = EntityState.Modified;
                        _ctx.SaveChanges();
                    }
                }
            }
        }

        public void InsertTrack(Spotify.AlbumRoot album)
        {
            List<Models.BackEnd.Album> DistintAlbum = _ctx.Albums.ToList();

            if (DistintAlbum == null)
                DistintAlbum = new List<Models.BackEnd.Album>();

            foreach (var x in album.Albums)
            {
                var albumGood = new Models.BackEnd.Album(x);
                albumGood.Songs = null;

                if (!DistintAlbum.Any(z => z.Name == x.Name))
                {
                    DistintAlbum.Add(albumGood);

                    _ctx.Albums.Add(albumGood);
                    _ctx.SaveChanges();
                }
            }
            _ctx.SaveChanges();
        }
    }
}
