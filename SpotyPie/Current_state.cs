using Newtonsoft.Json;
using SpotyPie.Models;
using Square.Picasso;
using System.Collections.Generic;
using System.Linq;

namespace SpotyPie
{
    public static class Current_state
    {
        public static bool IsPlaying = false;

        public static bool Start_music { get; set; } = false;

        public static bool IsPlayerLoaded { get; set; } = false;

        public static string ArtistName { get; set; }
        public static string SongTitle { get; set; }
        public static string AlbumTitle { get; set; }
        public static float Progress { get; set; }

        public static Android.Support.V4.App.Fragment BackFragment { get; set; }

        public static BlockWithImage ClickedInRVH { get; set; } = null;

        public static Artist Current_Artist { get; set; } = null;

        public static Item Current_Song { get; set; } = null;

        public static void SetSong(Item song)
        {
            Current_Song = song;
            ArtistName = JsonConvert.DeserializeObject<List<Artist>>(song.Artists).First().Name;
            SongTitle = song.Name;
            Start_music = true;

            Picasso.With(Player.contextStatic).Load(ClickedInRVH.Image).Into(Player.Player_Image);
            Player.Player_song_name.Text = SongTitle;
            Player.Player_artist_name.Text = ArtistName;
            Player.Player_playlist_name.Text = ArtistName + " - " + AlbumTitle;


            MainActivity.PlayerContainer.TranslationX = 0;
            Player.MusicPlayer();
        }

        public static void SetArtist(Artist art)
        {
            Current_Artist = art;
        }

        public static void SetAlbum(BlockWithImage album)
        {
            ClickedInRVH = album;
            AlbumTitle = album.Title;
        }
    }
}