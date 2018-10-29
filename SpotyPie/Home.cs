using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using SpotyPie.Helpers;
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
    public class Home : SupportFragment
    {
        View RootView;

        //Recent albums
        public static RecycleViewList<string> RecentAlbums = new RecycleViewList<string>();
        private RecyclerView.LayoutManager RecentAlbumsLayoutManager;
        private static RecyclerView.Adapter RecentAlbumsAdapter;
        private static RecyclerView RecentAlbumsRecyclerView;

        //Best albums
        public static RecycleViewList<string> BestAlbums = new RecycleViewList<string>();
        private RecyclerView.LayoutManager BestAlbumsLayoutManager;
        private static RecyclerView.Adapter BestAlbumsAdapter;
        private static RecyclerView BestAlbumsRecyclerView;

        //Jump back albums
        public static RecycleViewList<string> JumpBack = new RecycleViewList<string>();
        private RecyclerView.LayoutManager JumpBackLayoutManager;
        private static RecyclerView.Adapter JumpBackAdapter;
        private static RecyclerView JumpBackRecyclerView;

        //Top playlist
        public static RecycleViewList<string> TopPlaylist = new RecycleViewList<string>();
        private RecyclerView.LayoutManager TopPlaylistLayoutManager;
        private static RecyclerView.Adapter TopPlaylistAdapter;
        private static RecyclerView TopPlaylistRecyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.home_layout, container, false);

            RecentAlbumsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            RecentAlbumsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.recent_rv);
            RecentAlbumsRecyclerView.SetLayoutManager(RecentAlbumsLayoutManager);
            RecentAlbumsAdapter = new HorizontalRV(RecentAlbums, RecentAlbumsRecyclerView);
            RecentAlbums.Adapter = RecentAlbumsAdapter;
            RecentAlbumsRecyclerView.SetAdapter(RecentAlbumsAdapter);

            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");


            BestAlbumsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            BestAlbumsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.best_artists_rv);
            BestAlbumsRecyclerView.SetLayoutManager(BestAlbumsLayoutManager);
            BestAlbumsAdapter = new HorizontalRV(BestAlbums, BestAlbumsRecyclerView);
            BestAlbums.Adapter = BestAlbumsAdapter;
            BestAlbumsRecyclerView.SetAdapter(BestAlbumsAdapter);

            BestAlbums.Add("das");
            BestAlbums.Add("das");
            BestAlbums.Add("das");


            JumpBackLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            JumpBackRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.albums_old_rv);
            BestAlbumsRecyclerView.SetLayoutManager(JumpBackLayoutManager);
            JumpBackAdapter = new HorizontalRV(JumpBack, JumpBackRecyclerView);
            JumpBack.Adapter = JumpBackAdapter;
            JumpBackRecyclerView.SetAdapter(JumpBackAdapter);

            JumpBack.Add("das");
            JumpBack.Add("das");
            JumpBack.Add("das");


            TopPlaylistLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            TopPlaylistRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.playlist_rv);
            BestAlbumsRecyclerView.SetLayoutManager(TopPlaylistLayoutManager);
            TopPlaylistAdapter = new HorizontalRV(TopPlaylist, TopPlaylistRecyclerView);
            TopPlaylist.Adapter = TopPlaylistAdapter;
            TopPlaylistRecyclerView.SetAdapter(TopPlaylistAdapter);

            TopPlaylist.Add("das");
            TopPlaylist.Add("das");
            TopPlaylist.Add("das");


            return RootView;
        }

        public void GetRecentAlbums()
        {
            try
            {

            }
            catch (Exception e)
            {

            }
        }
    }
}