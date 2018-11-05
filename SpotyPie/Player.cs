using Android.App;
using Android.Media;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Threading.Tasks;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace SpotyPie
{
    public class Player : SupportFragment
    {
        View RootView;
        ImageButton HidePlayerButton;
        public static ImageButton PlayToggle;
        public static MediaPlayer player;

        public static TextView CurretSongTimeText;
        TextView TotalSongTimeText;

        TimeSpan CurrentTime = new TimeSpan(0, 0, 0, 0);
        TimeSpan TotalSongTime = new TimeSpan(0, 0, 0, 0);

        public static Android.Content.Context contextStatic;

        public ProgressBar SongProgress;

        public static ImageView Player_Image;
        public static TextView Player_song_name;
        public static TextView Player_artist_name;
        public static TextView Player_playlist_name; //July talk - Touch

        ImageButton Repeat;
        int Repeat_state = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.player, container, false);
            contextStatic = this.Context;

            Repeat = RootView.FindViewById<ImageButton>(Resource.Id.repeat);
            Repeat.Click += Repeat_Click;

            Player_Image = RootView.FindViewById<ImageView>(Resource.Id.album_image);
            Player_song_name = RootView.FindViewById<TextView>(Resource.Id.song_name);
            Player_artist_name = RootView.FindViewById<TextView>(Resource.Id.artist_name);
            Player_playlist_name = RootView.FindViewById<TextView>(Resource.Id.playlist_name);

            SongProgress = RootView.FindViewById<ProgressBar>(Resource.Id.song_progress);
            CurretSongTimeText = RootView.FindViewById<TextView>(Resource.Id.current_song_time);
            TotalSongTimeText = RootView.FindViewById<TextView>(Resource.Id.total_song_time);
            TotalSongTimeText.Visibility = ViewStates.Invisible;

            player = new MediaPlayer();
            player.Prepared += Player_Prepared;
            player.BufferingUpdate += Player_BufferingUpdate;
            StartPlayMusic();

            HidePlayerButton = RootView.FindViewById<ImageButton>(Resource.Id.back_button);
            PlayToggle = RootView.FindViewById<ImageButton>(Resource.Id.play_stop);

            if (Current_state.IsPlaying)
                PlayToggle.SetImageResource(Resource.Drawable.pause);
            else
                PlayToggle.SetImageResource(Resource.Drawable.play_button);

            HidePlayerButton.Click += HidePlayerButton_Click;
            PlayToggle.Click += PlayToggle_Click;
            return RootView;
        }

        public static void StartPlayMusic()
        {
            Task.Run(() =>
            {
                try
                {
                    if (Current_state.Start_music)
                    {
                        Application.SynchronizationContext.Post(_ =>
                            {
                                player.Reset();
                                player.SetAudioStreamType(Stream.Music);
                                player.SetDataSource("http://spotypie.deveim.com/api/stream/play/" + Current_state.Current_Song.Id);
                                player.Prepare();
                            }, null);
                    }
                }
                catch (Exception e)
                {
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Toast.MakeText(contextStatic, "Cant play " + Current_state.Current_Song.Id.ToString(), ToastLength.Short).Show();
                    }, null);
                }
            });
        }

        private void PlayToggle_Click(object sender, EventArgs e)
        {
            Current_state.Music_play_toggle();
        }

        private void HidePlayerButton_Click(object sender, EventArgs e)
        {
            Current_state.Player_visiblibity_toggle();
        }

        #region Player events
        private void Player_Prepared(object sender, EventArgs e)
        {
            TotalSongTimeText.Visibility = ViewStates.Visible;
            TimeSpan Time = new TimeSpan(0, 0, (int)player.Duration / 1000);
            TotalSongTimeText.Text = Time.Minutes + ":" + (Time.Seconds > 9 ? Time.Seconds.ToString() : "0" + Time.Seconds);

            if (Current_state.Start_music)
            {
                PlayToggle.SetImageResource(Resource.Drawable.pause);
                player.Start();
            }
        }

        private void Player_BufferingUpdate(object sender, MediaPlayer.BufferingUpdateEventArgs e)
        {
            try
            {
                //Toast.MakeText(this.Context, "Pasotion -" + player.CurrentPosition + " - " + player.Duration, ToastLength.Short).Show();
                var progress = (int)(player.CurrentPosition * 100) / player.Duration;
                SongProgress.Progress = (int)progress;

                if (CurrentTime.Seconds < (int)player.CurrentPosition / 1000)
                {
                    CurrentTime = new TimeSpan(0, 0, (int)(player.CurrentPosition / 1000));
                    CurretSongTimeText.Text = CurrentTime.Minutes + ":" + (CurrentTime.Seconds > 9 ? CurrentTime.Seconds.ToString() : "0" + CurrentTime.Seconds);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Player_Error(object sender, MediaPlayer.ErrorEventArgs e)
        {
            Toast.MakeText(this.Context, "Player error", ToastLength.Short).Show();
            //player.Reset();
        }
        #endregion


        private void Repeat_Click(object sender, EventArgs e)
        {
            switch (Repeat_state)
            {
                case 0:
                    {
                        Repeat.SetBackgroundResource(Resource.Drawable.repeat);
                        Repeat_state = 1;
                        break;
                    }
                case 1:
                    {
                        Repeat.SetBackgroundResource(Resource.Drawable.repeat_once);
                        Repeat_state = 2;
                        break;
                    }
                case 2:
                    {
                        Repeat.SetBackgroundResource(Resource.Drawable.repeat_off);
                        Repeat_state = 0;
                        break;
                    }
            }
        }

    }
}