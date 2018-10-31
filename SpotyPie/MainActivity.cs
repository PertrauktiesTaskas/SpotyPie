using Android.App;
using Android.OS;
using Android.Support.Constraints;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupportFragment = Android.Support.V4.App.Fragment;
using SupportFragmentManager = Android.Support.V4.App.FragmentManager;

namespace SpotyPie
{
    [Activity(Label = "SpotyPie", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true, Icon = "@drawable/logo", Theme = "@style/Theme.SpotyPie")]
    public class MainActivity : AppCompatActivity
    {
        SupportFragment Home;
        SupportFragment Browse;
        SupportFragment Search;
        SupportFragment Library;
        SupportFragment Player;
        SupportFragment Album;

        BottomNavigationView bottomNavigation;
        ImageButton PlayToggle;

        TextView ArtistName;
        TextView SongTitle;

        public static int widthInDp = 0;
        public static bool PlayerVisible = false;

        TextView ActionName;
        ConstraintLayout MiniPlayer;
        public static FrameLayout PlayerContainer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            PlayerContainer = FindViewById<FrameLayout>(Resource.Id.player_frame);

            widthInDp = Resources.DisplayMetrics.WidthPixels;
            PlayerContainer.TranslationX = 10000;

            Home = new Home();
            Browse = new Browse();
            Search = new Search();
            Library = new Library();
            Player = new Player();
            Album = new AlbumFragment();
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.player_frame, Player).Commit();

            PlayToggle = FindViewById<ImageButton>(Resource.Id.play_stop);
            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.NavBot);
            ActionName = FindViewById<TextView>(Resource.Id.textView);
            MiniPlayer = FindViewById<ConstraintLayout>(Resource.Id.PlayerContainer);
            ArtistName = FindViewById<TextView>(Resource.Id.artist_name);
            SongTitle = FindViewById<TextView>(Resource.Id.song_name);

            if (Current_state.IsPlaying)
                PlayToggle.SetImageResource(Resource.Drawable.pause);
            else
                PlayToggle.SetImageResource(Resource.Drawable.play_button);

            MiniPlayer.Click += MiniPlayer_Click;
            PlayToggle.Click += PlayToggle_Click;
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            LoadFragment(Resource.Id.home);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() => API_data.GetSong());
        }

        private void PlayToggle_Click(object sender, EventArgs e)
        {
            Current_state.IsPlaying = !Current_state.IsPlaying;

            if (Current_state.IsPlaying)
                PlayToggle.SetImageResource(Resource.Drawable.pause);
            else
                PlayToggle.SetImageResource(Resource.Drawable.play_button);
        }

        private void MiniPlayer_Click(object sender, EventArgs e)
        {
            PlayToggle_Click(null, null);
            PlayerContainer.TranslationX = 0;
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //int id = item.ItemId;
            //if (id == Resource.Id.action_settings)
            //{
            //    return true;
            //}

            return base.OnOptionsItemSelected(item);
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new Home(), "Home");
            adapter.AddFragment(new Browse(), "Browse");
            adapter.AddFragment(new Search(), "Search");
            adapter.AddFragment(new Library(), "Library");

            viewPager.Adapter = adapter;
        }

        public class TabAdapter : FragmentPagerAdapter
        {
            public List<SupportFragment> Fragments { get; set; }
            public List<string> FragmentsNames { get; set; }

            public TabAdapter(SupportFragmentManager sfm) : base(sfm)
            {
                Fragments = new List<SupportFragment>();
                FragmentsNames = new List<string>();
            }

            public void AddFragment(SupportFragment fragment, string name)
            {
                Fragments.Add(fragment);
                FragmentsNames.Add(name);
            }

            public override int Count => Fragments.Count;

            public override SupportFragment GetItem(int position)
            {
                return Fragments[position];
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(FragmentsNames[position]);
            }
        }

        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.home:
                    fragment = Album;
                    ActionName.Text = "Home";
                    break;
                case Resource.Id.browse:
                    fragment = Browse;
                    ActionName.Text = "Browse";
                    break;
                case Resource.Id.search:
                    fragment = Search;
                    ActionName.Text = "Search";
                    break;
                case Resource.Id.library:
                    fragment = Library;
                    ActionName.Text = "Library";
                    break;
            }

            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}

