using Android.App;
using Android.Views;
using Newtonsoft.Json;
using RestSharp;
using SpotyPie.Models;
using Square.Picasso;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static string Current_Player_Image { get; set; }

        public static Artist Current_Artist { get; set; } = null;

        public static Item Current_Song { get; set; } = null;

        public static List<Item> Current_Song_List { get; set; } = null;

        public static void SetSong(Item song, bool refresh = false)
        {
            Current_Song = song;
            Current_Song.Playing = true;
            Current_Song_List.First(x => x.Id == Current_Song.Id).Playing = true;
            ArtistName = JsonConvert.DeserializeObject<List<Artist>>(song.Artists).First().Name;
            SongTitle = song.Name;
            Start_music = true;
            PlayerIsVisible = true;
            UpdateCurrentInfo();
            Player.Player.StartPlayMusic();
            Task.Run(() => Update());

            async Task Update()
            {
                var client = new RestClient("http://spotypie.pertrauktiestaskas.lt/api/songs/1/update");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = await client.ExecuteTaskAsync(request);
            }
        }

        public static void UpdateCurrentInfo()
        {
            Task.Run(() =>
            {
                Application.SynchronizationContext.Post(_ =>
                {
                    if (Current_Player_Image != ClickedInRVH.Image)
                    {
                        Current_Player_Image = ClickedInRVH.Image;
                        Picasso.With(Player.Player.contextStatic).Load(ClickedInRVH.Image).Resize(300, 300).CenterCrop().Into(Player.Player.Player_Image);
                    }
                    MainActivity.PlayerContainer.TranslationX = 0;
                    Player.Player.CurretSongTimeText.Text = "0.00";
                    Player.Player.Player_song_name.Text = SongTitle;
                    MainActivity.SongTitle.Text = SongTitle;
                    MainActivity.ArtistName.Text = ArtistName;
                    Player.Player.Player_artist_name.Text = ArtistName;
                    Player.Player.Player_playlist_name.Text = ArtistName + " - " + AlbumTitle;
                }, null);
            });
        }

        public static void SetArtist(Artist art)
        {
            Current_Artist = art;
        }

        public static void SetAlbum(BlockWithImage album)
        {
            Task.Run(() => Update());
            ClickedInRVH = album;
            AlbumTitle = album.Title;

            async Task Update()
            {
                var client = new RestClient("http://spotypie.pertrauktiestaskas.lt/api/album/update/" + album.Id);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = await client.ExecuteTaskAsync(request);
            }
        }

        public static void Music_play_toggle()
        {
            Current_state.IsPlaying = !Current_state.IsPlaying;

            if (Current_state.IsPlaying)
            {
                MainActivity.PlayToggle.SetImageResource(Resource.Drawable.pause);
                Player.Player.PlayToggle.SetImageResource(Resource.Drawable.pause);
                Player.Player.player.Start();
            }
            else
            {
                MainActivity.PlayToggle.SetImageResource(Resource.Drawable.play_button);
                Player.Player.PlayToggle.SetImageResource(Resource.Drawable.play_button);
                if (Player.Player.player.IsPlaying)
                    Player.Player.player.Pause();
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

        public static void ChangeSong(bool Foward)
        {
            for (int i = 0; i < Current_Song_List.Count; i++)
            {
                if (Current_Song_List[i].Playing)
                {
                    if (Foward)
                    {
                        Current_Song_List[i].Playing = false;
                        if ((i + 1) == Current_Song_List.Count)
                        {
                            Current_Song_List[0].Playing = true;
                            SetSong(Current_Song_List[0]);
                        }
                        else
                        {
                            Current_Song_List[i + 1].Playing = true;
                            SetSong(Current_Song_List[i + 1]);
                        }
                    }
                    else
                    {
                        Current_Song_List[i].Playing = false;
                        if (i == 0)
                        {
                            Current_Song_List[0].Playing = true;
                            SetSong(Current_Song_List[0]);
                        }
                        else
                        {

                            Current_Song_List[i - 1].Playing = true;
                            SetSong(Current_Song_List[i - 1]);
                        }
                    }
                    break;
                }
            }
        }
    }
}