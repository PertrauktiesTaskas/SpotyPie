using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;
using SpotyPie.Helpers;
using SpotyPie.Models;
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
        public static RecycleViewList<BlockWithImage> RecentAlbums = new RecycleViewList<BlockWithImage>();
        private RecyclerView.LayoutManager RecentAlbumsLayoutManager;
        private static RecyclerView.Adapter RecentAlbumsAdapter;
        private static RecyclerView RecentAlbumsRecyclerView;

        //Best albums
        public static RecycleViewList<BlockWithImage> BestAlbums = new RecycleViewList<BlockWithImage>();
        private RecyclerView.LayoutManager BestAlbumsLayoutManager;
        private static RecyclerView.Adapter BestAlbumsAdapter;
        private static RecyclerView BestAlbumsRecyclerView;

        //Best artists
        public static RecycleViewList<BlockWithImage> BestArtists = new RecycleViewList<BlockWithImage>();
        private RecyclerView.LayoutManager BestArtistsLayoutManager;
        private static RecyclerView.Adapter BestArtistsAdapter;
        private static RecyclerView BestArtistsRecyclerView;

        //Jump back albums
        public static RecycleViewList<BlockWithImage> JumpBack = new RecycleViewList<BlockWithImage>();
        private RecyclerView.LayoutManager JumpBackLayoutManager;
        private static RecyclerView.Adapter JumpBackAdapter;
        private static RecyclerView JumpBackRecyclerView;

        //Top playlist
        public static RecycleViewList<BlockWithImage> TopPlaylist = new RecycleViewList<BlockWithImage>();
        private RecyclerView.LayoutManager TopPlaylistLayoutManager;
        private static RecyclerView.Adapter TopPlaylistAdapter;
        private static RecyclerView TopPlaylistRecyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.home_layout, container, false);

            //RECENT ALBUMS
            RecentAlbumsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            RecentAlbumsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.recent_rv);
            RecentAlbumsRecyclerView.SetLayoutManager(RecentAlbumsLayoutManager);
            RecentAlbumsAdapter = new HorizontalRV(RecentAlbums, RecentAlbumsRecyclerView, this.Context);
            RecentAlbums.Adapter = RecentAlbumsAdapter;
            RecentAlbumsRecyclerView.SetAdapter(RecentAlbumsAdapter);

            //MOST POLULAR ALL TIME ALBUMS
            BestAlbumsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            BestAlbumsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.best_albums_rv);
            BestAlbumsRecyclerView.SetLayoutManager(BestAlbumsLayoutManager);
            BestAlbumsAdapter = new HorizontalRV(BestAlbums, BestAlbumsRecyclerView, this.Context);
            BestAlbums.Adapter = BestAlbumsAdapter;
            BestAlbumsRecyclerView.SetAdapter(BestAlbumsAdapter);

            //MOST POPULAR ARTISTS
            BestArtistsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            BestArtistsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.best_artists_rv);
            BestArtistsRecyclerView.SetLayoutManager(BestArtistsLayoutManager);
            BestArtistsAdapter = new HorizontalRV(BestAlbums, BestArtistsRecyclerView, this.Context);
            BestArtists.Adapter = BestArtistsAdapter;
            BestArtistsRecyclerView.SetAdapter(BestArtistsAdapter);

            //OLD ALBUMS AND SONGS
            JumpBackLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            JumpBackRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.albums_old_rv);
            BestAlbumsRecyclerView.SetLayoutManager(JumpBackLayoutManager);
            JumpBackAdapter = new HorizontalRV(JumpBack, JumpBackRecyclerView, this.Context);
            JumpBack.Adapter = JumpBackAdapter;
            JumpBackRecyclerView.SetAdapter(JumpBackAdapter);

            //MOST POLUPAR USER PLAYLISTS
            TopPlaylistLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            TopPlaylistRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.playlist_rv);
            BestAlbumsRecyclerView.SetLayoutManager(TopPlaylistLayoutManager);
            TopPlaylistAdapter = new HorizontalRV(TopPlaylist, TopPlaylistRecyclerView, this.Context);
            TopPlaylist.Adapter = TopPlaylistAdapter;
            TopPlaylistRecyclerView.SetAdapter(TopPlaylistAdapter);

            Task.Run(() => GetRecentAlbumsAsync(this.Context));
            Task.Run(() => GetPolularAlbumsAsync(this.Context));
            return RootView;
        }

        public async Task GetRecentAlbumsAsync(Context cnt)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/api/album/Recent");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    List<Album> album = JsonConvert.DeserializeObject<List<Album>>(response.Content);
                    Application.SynchronizationContext.Post(_ =>
                    {
                        foreach (var x in album)
                        {
                            RecentAlbums.Add(new BlockWithImage(x.Name, x.Label, x.Images.First().Url));
                        }
                    }, null);
                }
                else
                {
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Toast.MakeText(this.Context, "Recent Albums API error", ToastLength.Short).Show();
                    }, null);
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task GetPolularAlbumsAsync(Context cnt)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/api/album/popular");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    List<Album> album = JsonConvert.DeserializeObject<List<Album>>(response.Content);
                    Application.SynchronizationContext.Post(_ =>
                    {
                        foreach (var x in album)
                        {
                            BestAlbums.Add(new BlockWithImage(x.Name, x.Label, x.Images.First().Url));
                        }
                    }, null);
                }
                else
                {
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Toast.MakeText(this.Context, "Recent Albums API error", ToastLength.Short).Show();
                    }, null);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}