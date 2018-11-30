using Android.App;
using Android.OS;
using Android.Support.Constraints;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
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
        public static SupportFragment Album;
        public static SupportFragment Artist;

        BottomNavigationView bottomNavigation;
        public static ImageButton PlayToggle;

        public static TextView ArtistName;
        public static TextView SongTitle;
        public static ImageButton BackHeaderButton;
        public static ImageButton OptionsHeaderButton;

        public static int widthInDp = 0;
        public static bool PlayerVisible = false;

        public static TextView ActionName;
        ConstraintLayout MiniPlayer;
        public static FrameLayout PlayerContainer;

        ConstraintLayout HeaderContainer;

        public static Android.Support.V4.App.FragmentManager mSupportFragmentManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            HeaderContainer = FindViewById<ConstraintLayout>(Resource.Id.HeaderContainer);

            mSupportFragmentManager = SupportFragmentManager;

            PlayerContainer = FindViewById<FrameLayout>(Resource.Id.player_frame);

            widthInDp = Resources.DisplayMetrics.WidthPixels;
            PlayerContainer.TranslationX = widthInDp;

            Home = new Home();
            Browse = new Browse();
            Search = new Search();
            Library = new LibraryFragment();
            Player = new Player.Player();
            Album = new AlbumFragment();
            Artist = new ArtistFragment();

            PlayToggle = FindViewById<ImageButton>(Resource.Id.play_stop);
            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.NavBot);
            ActionName = FindViewById<TextView>(Resource.Id.textView);
            MiniPlayer = FindViewById<ConstraintLayout>(Resource.Id.PlayerContainer);
            ArtistName = FindViewById<TextView>(Resource.Id.artist_name);
            //Animation marquee = AnimationUtils.LoadAnimation(this, Resource.Drawable.marquee);
            //ArtistName.StartAnimation(marquee);

            SongTitle = FindViewById<TextView>(Resource.Id.song_name);
            BackHeaderButton = FindViewById<ImageButton>(Resource.Id.back);
            OptionsHeaderButton = FindViewById<ImageButton>(Resource.Id.options);

            if (Current_state.IsPlaying)
                PlayToggle.SetImageResource(Resource.Drawable.pause);
            else
                PlayToggle.SetImageResource(Resource.Drawable.play_button);

            BackHeaderButton.Click += BackHeaderButton_Click;

            MiniPlayer.Click += MiniPlayer_Click;
            PlayToggle.Click += PlayToggle_Click;
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            LoadFragment(Resource.Id.home);
        }

        public override void OnBackPressed()
        {
            if (Current_state.PlayerIsVisible)
            {
                PlayerContainer.TranslationX = widthInDp;
                return;
            }
            base.OnBackPressed();
        }

        private void BackHeaderButton_Click(object sender, EventArgs e)
        {
            try
            {
                Current_state.HideHeaderNavigationButtons();
                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, Current_state.BackFragment)
                    .Commit();
            }
            catch (System.Exception)
            {
                Home = new Home();
                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, Home)
                    .Commit();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (!Current_state.IsPlayerLoaded)
            {
                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.player_frame, Player)
                    .Commit();
                PlayerContainer.TranslationX = widthInDp;
            }
            Task.Run(() => API_data.GetSong());
        }

        private void PlayToggle_Click(object sender, EventArgs e)
        {
            Current_state.Music_play_toggle();
        }

        private void MiniPlayer_Click(object sender, EventArgs e)
        {
            Current_state.Player_visiblibity_toggle();
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
            return base.OnOptionsItemSelected(item);
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            TabAdapter adapter = new TabAdapter(SupportFragmentManager);
            adapter.AddFragment(new Home(), "Home");
            adapter.AddFragment(new Browse(), "Browse");
            adapter.AddFragment(new Search(), "Search");
            adapter.AddFragment(new LibraryFragment(), "Library");

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
            if (HeaderContainer.Visibility == ViewStates.Gone)
                HeaderContainer.Visibility = ViewStates.Visible;

            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.home:
                    fragment = Home;
                    ActionName.Text = "Home";
                    break;
                case Resource.Id.browse:
                    fragment = Browse;
                    ActionName.Text = "Browse";
                    break;
                case Resource.Id.search:
                    fragment = Search;
                    HeaderContainer.Visibility = ViewStates.Gone;
                    break;
                case Resource.Id.library:
                    fragment = Library;
                    ActionName.Text = "Library";
                    break;
            }

            if (fragment == null)
                return;

            Current_state.BackFragment = fragment;

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }
    }
}

