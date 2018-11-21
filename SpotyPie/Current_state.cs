using Android.Views;
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

        public static bool PlayerIsVisible { get; set; } = false;

        public static string ArtistName { get; set; }
        public static string SongTitle { get; set; }
        public static string AlbumTitle { get; set; }
        public static float Progress { get; set; }

        public static Android.Support.V4.App.Fragment BackFragment { get; set; }

        public static BlockWithImage ClickedInRVH { get; set; } = null;

        public static Artist Current_Artist { get; set; } = null;

        public static Item Current_Song { get; set; } = null;

        public static void SetSong(Item song, bool refresh = false)
        {
            Current_Song = song;
            ArtistName = JsonConvert.DeserializeObject<List<Artist>>(song.Artists).First().Name;
            SongTitle = song.Name;
            Start_music = true;
            PlayerIsVisible = true;

            Picasso.With(Player.contextStatic).Load(ClickedInRVH.Image).Into(Player.Player_Image);
            Player.CurretSongTimeText.Text = "0.00";
            UpdateCurrentInfo();
            MainActivity.PlayerContainer.TranslationX = 0;
            Player.StartPlayMusic();
        }

        public static void UpdateCurrentInfo()
        {
            Player.Player_song_name.Text = SongTitle;
            MainActivity.SongTitle.Text = SongTitle;
            MainActivity.ArtistName.Text = ArtistName;
            Player.Player_artist_name.Text = ArtistName;
            Player.Player_playlist_name.Text = ArtistName + " - " + AlbumTitle;
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

        public static void Music_play_toggle()
        {
            Current_state.IsPlaying = !Current_state.IsPlaying;

            if (Current_state.IsPlaying)
            {
                MainActivity.PlayToggle.SetImageResource(Resource.Drawable.pause);
                Player.PlayToggle.SetImageResource(Resource.Drawable.pause);
                Player.player.Start();
            }
            else
            {
                MainActivity.PlayToggle.SetImageResource(Resource.Drawable.play_button);
                Player.PlayToggle.SetImageResource(Resource.Drawable.play_button);
                Player.player.Pause();
            }
        }

        public static void Player_visiblibity_toggle()
        {
            if (PlayerIsVisible)
            {
                PlayerIsVisible = false;
                MainActivity.PlayerContainer.TranslationX = MainActivity.widthInDp;
            }
            else
            {
                PlayerIsVisible = true;
                MainActivity.PlayerContainer.TranslationX = 0;
            }


        }

        public static void ShowHeaderNavigationButtons()
        {
            MainActivity.BackHeaderButton.Visibility = ViewStates.Visible;
            MainActivity.OptionsHeaderButton.Visibility = ViewStates.Visible;
        }

        public static void HideHeaderNavigationButtons()
        {
            MainActivity.BackHeaderButton.Visibility = ViewStates.Gone;
            MainActivity.OptionsHeaderButton.Visibility = ViewStates.Gone;
        }
    }
}