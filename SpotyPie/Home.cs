using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;
using SpotyPie.Helpers;
using SpotyPie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            RecentAlbumsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (RecentAlbumsRecyclerView != null && RecentAlbumsRecyclerView.ChildCount != 0)
                {
                    Current_state.ClickedInRVH = RecentAlbums[position];
                    FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, MainActivity.Album)
                    .Commit();
                }
            });



            //MOST POLULAR ALL TIME ALBUMS
            BestAlbumsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            BestAlbumsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.best_albums_rv);
            BestAlbumsRecyclerView.SetLayoutManager(BestAlbumsLayoutManager);
            BestAlbumsAdapter = new HorizontalRV(BestAlbums, BestAlbumsRecyclerView, this.Context);
            BestAlbums.Adapter = BestAlbumsAdapter;
            BestAlbumsRecyclerView.SetAdapter(BestAlbumsAdapter);

            BestAlbumsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (BestAlbumsRecyclerView != null && BestAlbumsRecyclerView.ChildCount != 0)
                {
                    Current_state.ClickedInRVH = BestAlbums[position];
                    FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, MainActivity.Album)
                    .Commit();
                }
            });



            //MOST POPULAR ARTISTS
            BestArtistsLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            BestArtistsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.best_artists_rv);
            BestArtistsRecyclerView.SetLayoutManager(BestArtistsLayoutManager);
            BestArtistsAdapter = new HorizontalRV(BestArtists, BestArtistsRecyclerView, this.Context);
            BestArtists.Adapter = BestArtistsAdapter;
            BestArtistsRecyclerView.SetAdapter(BestArtistsAdapter);

            BestArtistsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (BestArtistsRecyclerView != null && BestArtistsRecyclerView.ChildCount != 0)
                {
                    Toast.MakeText(this.Context, "Dar nesukurta artist", ToastLength.Short).Show();
                }
            });

            //OLD ALBUMS AND SONGS
            JumpBackLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            JumpBackRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.albums_old_rv);
            BestAlbumsRecyclerView.SetLayoutManager(JumpBackLayoutManager);
            JumpBackAdapter = new HorizontalRV(JumpBack, JumpBackRecyclerView, this.Context);
            JumpBack.Adapter = JumpBackAdapter;
            JumpBackRecyclerView.SetAdapter(JumpBackAdapter);

            JumpBackRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (JumpBackRecyclerView != null && JumpBackRecyclerView.ChildCount != 0)
                {
                    Current_state.ClickedInRVH = JumpBack[position];
                    FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, MainActivity.Album)
                    .Commit();
                }
            });


            //MOST POLUPAR USER PLAYLISTS
            TopPlaylistLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            TopPlaylistRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.playlist_rv);
            BestAlbumsRecyclerView.SetLayoutManager(TopPlaylistLayoutManager);
            TopPlaylistAdapter = new HorizontalRV(TopPlaylist, TopPlaylistRecyclerView, this.Context);
            TopPlaylist.Adapter = TopPlaylistAdapter;
            TopPlaylistRecyclerView.SetAdapter(TopPlaylistAdapter);

            TopPlaylistRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (TopPlaylistRecyclerView != null && TopPlaylistRecyclerView.ChildCount != 0)
                {
                    Toast.MakeText(this.Context, "Dar nesukurta playlist", ToastLength.Short).Show();
                }
            });


            Task.Run(() => GetRecentAlbumsAsync(this.Context));
            Task.Run(() => GetPolularAlbumsAsync(this.Context));
            Task.Run(() => GetPolularArtistsAsync(this.Context));
            Task.Run(() => GetOldAlbumsAsync(this.Context));

            return RootView;
        }

        public override void OnDestroyView()
        {
            RecentAlbums = new RecycleViewList<BlockWithImage>();
            BestAlbums = new RecycleViewList<BlockWithImage>();
            BestArtists = new RecycleViewList<BlockWithImage>();
            JumpBack = new RecycleViewList<BlockWithImage>();
            TopPlaylist = new RecycleViewList<BlockWithImage>();
            base.OnDestroyView();
        }

        public override void OnResume()
        {
            base.OnResume();
            if (RecentAlbums == null || RecentAlbums.Count == 0)
                Task.Run(() => GetRecentAlbumsAsync(this.Context));

            if (BestAlbums == null || BestAlbums.Count == 0)
                Task.Run(() => GetPolularAlbumsAsync(this.Context));

            if (BestArtists == null || BestArtists.Count == 0)
                Task.Run(() => GetPolularArtistsAsync(this.Context));

            if (JumpBack == null || JumpBack.Count == 0)
                Task.Run(() => GetOldAlbumsAsync(this.Context));

            //if (TopPlaylist == null || TopPlaylist.Count == 0)
            //Todo add playlist call
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
                            RecentAlbums.Add(new BlockWithImage(x.Id, RvType.Album, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name, x.Images.First().Url));
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
                            BestAlbums.Add(new BlockWithImage(x.Id, RvType.Album, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name, x.Images.First().Url));
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

        public async Task GetPolularArtistsAsync(Context cnt)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/api/artist/popular");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    List<Artist> artists = JsonConvert.DeserializeObject<List<Artist>>(response.Content);
                    Application.SynchronizationContext.Post(_ =>
                    {
                        foreach (var x in artists)
                        {
                            string DisplayGenre;
                            var genres = JsonConvert.DeserializeObject<List<string>>(x.Genres);
                            if (genres.Count > 1)
                            {
                                Random ran = new Random();
                                int index = ran.Next(0, genres.Count - 1);
                                DisplayGenre = genres[index];
                            }
                            else if (genres.Count == 1)
                            {
                                DisplayGenre = genres[0];
                            }
                            else
                                DisplayGenre = string.Empty;

                            BestArtists.Add(new BlockWithImage(x.Id, RvType.Artist, x.Name, DisplayGenre, x.Images.First().Url));
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

        public async Task GetOldAlbumsAsync(Context cnt)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/api/album/old");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    List<Album> album = JsonConvert.DeserializeObject<List<Album>>(response.Content);
                    Application.SynchronizationContext.Post(_ =>
                    {
                        foreach (var x in album)
                        {
                            JumpBack.Add(new BlockWithImage(x.Id, RvType.Album, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name, x.Images.First().Url));
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