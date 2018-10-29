using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace SpotyPie
{
    public class Player : SupportFragment
    {
        View RootView;
        ImageButton HidePlayerButton;
        ImageButton PlayToggle;
        public static MediaPlayer player;
        TextView CurretSongTimeText;
        TextView TotalSongTimeText;
        TimeSpan CurrentTime = new TimeSpan(0, 0, 0, 0);
        TimeSpan TotalSongTime = new TimeSpan(0, 0, 0, 0);

        public ProgressBar SongProgress;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.player, container, false);

            SongProgress = RootView.FindViewById<ProgressBar>(Resource.Id.song_progress);
            CurretSongTimeText = RootView.FindViewById<TextView>(Resource.Id.current_song_time);
            TotalSongTimeText = RootView.FindViewById<TextView>(Resource.Id.total_song_time);
            TotalSongTimeText.Visibility = ViewStates.Invisible;

            player = new MediaPlayer();
            player.Prepared += Player_Prepared;
            player.BufferingUpdate += Player_BufferingUpdate;
            MusicPlayer("");

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

        private void Player_Prepared(object sender, EventArgs e)
        {
            TotalSongTimeText.Visibility = ViewStates.Visible;
            TimeSpan Time = new TimeSpan(0, 0, (int)player.Duration / 1000);
            TotalSongTimeText.Text = Time.Minutes + ":" + (Time.Seconds > 9 ? Time.Seconds.ToString() : "0" + Time.Seconds);
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

        public void MusicPlayer(string url)
        {
            player.SetAudioStreamType(Stream.Music);
            player.SetDataSource("http://spotypie.deveim.com/api/stream/play/542");
            player.Prepare();
        }

        private void PlayToggle_Click(object sender, EventArgs e)
        {
            Current_state.IsPlaying = !Current_state.IsPlaying;

            if (Current_state.IsPlaying)
            {
                PlayToggle.SetImageResource(Resource.Drawable.pause);
                player.Start();
            }
            else
            {
                PlayToggle.SetImageResource(Resource.Drawable.play_button);
                player.Pause();
            }
        }

        private void HidePlayerButton_Click(object sender, EventArgs e)
        {
            PlayToggle_Click(null, null);
            MainActivity.PlayerContainer.TranslationX = MainActivity.widthInDp;
        }

    }
}