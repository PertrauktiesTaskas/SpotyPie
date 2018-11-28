using Config.Net;
using Database;
using IdSharp.Tagging.VorbisComment;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.BackEnd;
using Newtonsoft.Json;
using Service.Helpers;
using Service.Settings;
using Services.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spotify = Models.Spotify;

namespace Services
{
    public class Service : IDb
    {
        private readonly SpotyPieIDbContext _ctx;
        private ISettings settings;
        public Service(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;
            settings = new ConfigurationBuilder<ISettings>()
                .UseJsonFile(Environment.CurrentDirectory + @"/settings.json")
                .Build();
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

        public string ConvertAudio(string path, int quality)
        {
            var outputDir = settings != null ? settings.AudioCachePath : @"/root/MusicCache";
            if (!string.IsNullOrWhiteSpace(path))
            {
                var fileName = Path.GetFileName(path);
                var outputFileName = Path.GetFileNameWithoutExtension(path) + quality.ToString() + ".mp3";

                if (!File.Exists(outputDir + outputFileName))
                {
                    var command = string.Format("ffmpeg -i \"{0}\" -ab {1}k \"{2}\"", path, quality, outputDir + outputFileName);
                    try
                    {
                        var output = command.Bash();
                        if (File.Exists(outputDir + outputFileName))
                            return outputDir + outputFileName;
                        else
                            return "";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                    return outputDir + outputFileName;
            }
            else
                return "";
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

        public async Task<bool> AddAudioToLibrary(string path, string name, Item file = null)
        {
            try
            {
                Item audioDb = null;
                IAudioFile audio = AudioFile.Create(path, false);

                if (audio != null && audio.FileType == AudioFileType.Flac)
                {
                    VorbisComment flacTag = new VorbisComment(path);

                    if (string.IsNullOrWhiteSpace(flacTag.Title) || string.IsNullOrWhiteSpace(flacTag.Artist))
                        audioDb = await _ctx.Items
                            .FirstOrDefaultAsync(x =>
                            x.Name.Equals(Path.GetFileNameWithoutExtension(name), StringComparison.InvariantCultureIgnoreCase));
                    else
                    {
                        var replaced = flacTag.Title.Replace("'", "’");
                        var replacedArtist = flacTag.Artist.Replace("'", "’");
                        audioDb = await _ctx.Items
                            .FirstOrDefaultAsync(x => x.Name.Equals(replaced, StringComparison.InvariantCultureIgnoreCase)
                            && x.Artists.Contains(replacedArtist));
                    }
                }
                else
                    audioDb = await _ctx.Items.FirstOrDefaultAsync(x => x.Name.Equals(Path.GetFileNameWithoutExtension(name), StringComparison.InvariantCultureIgnoreCase));

                if (audioDb != null)
                {
                    _ctx.Update(audioDb);
                    audioDb.LocalUrl = path;

                    if (audio != null)
                    {
                        audioDb.DurationMs = (long)audio.TotalSeconds * 1000;
                    }

                    _ctx.SaveChanges();
                    audio = null;

                    return true;
                }
                return false;
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

        public async Task<List<Album>> GetAlbumsByArtist(int id)
        {
            try
            {
                var albums = await _ctx.Artists
                    .Include(x => x.Albums)
                    .FirstOrDefaultAsync(x => x.Id == id);
                return albums.Albums;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void RemoveCache()
        {
            DirectoryInfo di = new DirectoryInfo(settings.CachePath);
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
        }

        public async Task<string> CacheImages()
        {
            HttpClient client = new HttpClient();
            var rgx = new Regex(@"^(http|https)://");
            var imgList = await _ctx.Images.Where(x => rgx.IsMatch(x.Url)).ToListAsync();
            int savedCount = 0;

            foreach (var img in imgList)
            {
                var response = await client.GetAsync(img.Url);

                var filename = Path.GetRandomFileName();
                var path = settings.CachePath + filename;

                if (response.IsSuccessStatusCode)
                {
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        var content = await response.Content.ReadAsByteArrayAsync();
                        await stream.WriteAsync(content);
                    }

                    if (File.Exists(path))
                    {
                        _ctx.Update(img);
                        img.Url = "http://cdn.spotypie.deveim.com/" + filename;
                        if (_ctx.SaveChanges() == 1)
                            savedCount++;
                    }
                }
            }

            return string.Format("{0}/{1}", savedCount, imgList.Count);
        }

        public int GetCPUUsage()
        {
            var output = @"awk -v a=""$(awk '/cpu /{print $2+$4,$2+$4+$5}' / proc / stat; sleep 1)"" '/cpu /{split(a,b,"" ""); print 100*($2+$4-b[1])/($2+$4+$5-b[2])}'  /proc/stat"
                .Bash();
            return double.TryParse(output, out double dPercent) ? Convert.ToInt32(dPercent) : -1;
        }

        public int GetRAMUsage()
        {
            var output = "free | awk 'FNR == 3 {print $3/($3+$4)*100}'"
                .Bash();
            return double.TryParse(output, out double dPercent) ? Convert.ToInt32(dPercent) : -1;
        }

        public int GetCPUTemperature()
        {
            var output = @"sensors 2>/dev/null | awk '/id 0:/{printf "" % d\n"", $4}'"
                  .Bash();
            return int.TryParse(output, out int dPercent) ? dPercent : -1;
        }

        public int GetUsedStorage()
        {
            var output = "df --output=pcent | awk -F'%' 'NR==2{print $1}'"
                  .Bash();
            return int.TryParse(output, out int dPercent) ? dPercent : -1;
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
