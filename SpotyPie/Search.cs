using Android.App;
using Android.OS;
using Android.Support.Constraints;
using Android.Support.Design.Widget;
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
    public class Search : SupportFragment
    {
        View RootView;

        //List Songs
        public List<Item> SearchSongs = new List<Item>();
        public RecycleViewList<List> Songs = new RecycleViewList<List>();
        private RecyclerView.LayoutManager AlbumSongsLayoutManager;
        private RecyclerView.Adapter AlbumSongsAdapter;
        private RecyclerView AlbumSongsRecyclerView;

        public RecycleViewList<TwoBlockWithImage> Albums = new RecycleViewList<TwoBlockWithImage>();
        private RecyclerView.LayoutManager AlbumsLayoutManager;
        private RecyclerView.Adapter AlbumsAdapter;
        private RecyclerView AlbumsRecyclerView;

        public RecycleViewList<TwoBlockWithImage> Artists = new RecycleViewList<TwoBlockWithImage>();
        private RecyclerView.LayoutManager ArtistsLayoutManager;
        private RecyclerView.Adapter ArtistsAdapter;
        private RecyclerView ArtistsRecyclerView;

        private bool IsSearchResultEmpty = true;
        private bool SongFinded = false;
        private bool AlbumFinded = false;
        private bool ArtistFinded = false;
        private bool PlaylistFinded = false;

        ConstraintLayout SongsContainer;
        ConstraintLayout AlbumsContainer;
        ConstraintLayout PlaylistContainer;
        ConstraintLayout ArtistContainer;

        FrameLayout SearchEmpty;

        EditText search;
        ImageView SearchIcon;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.search_layout, container, false);

            SongsContainer = RootView.FindViewById<ConstraintLayout>(Resource.Id.constraintLayoutSongs);
            AlbumsContainer = RootView.FindViewById<ConstraintLayout>(Resource.Id.constraintLayoutAlbums);
            ArtistContainer = RootView.FindViewById<ConstraintLayout>(Resource.Id.ConstrainLayoutArtist);
            SearchEmpty = RootView.FindViewById<FrameLayout>(Resource.Id.searchStartx);
            var layoutParrams = SearchEmpty.LayoutParameters;
            layoutParrams.Height = MainActivity.HeightInDp;
            SearchEmpty.LayoutParameters = layoutParrams;

            SearchIcon = RootView.FindViewById<ImageView>(Resource.Id.imageView);

            search = RootView.FindViewById<EditText>(Resource.Id.editText);
            search.BeforeTextChanged += Search_BeforeTextChanged;
            search.FocusChange += Search_FocusChange;
            // song list
            AlbumSongsLayoutManager = new LinearLayoutManager(this.Activity);
            AlbumSongsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.song_rv);
            AlbumSongsRecyclerView.SetLayoutManager(AlbumSongsLayoutManager);
            AlbumSongsAdapter = new VerticalRV(Songs, AlbumSongsRecyclerView, this.Context);
            Songs.Adapter = AlbumSongsAdapter;
            AlbumSongsRecyclerView.SetAdapter(AlbumSongsAdapter);
            AlbumSongsRecyclerView.NestedScrollingEnabled = false;

            AlbumSongsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (AlbumSongsRecyclerView != null && AlbumSongsRecyclerView.ChildCount != 0)
                {
                    //Current_state.SetSong(Current_state.Current_Song_List[position]);
                }
            });

            AlbumsLayoutManager = new LinearLayoutManager(this.Activity);
            AlbumsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.albums_rv);
            AlbumsRecyclerView.SetLayoutManager(AlbumsLayoutManager);
            AlbumsAdapter = new BoxedRV(Albums, AlbumsRecyclerView, this.Context);
            Albums.Adapter = AlbumsAdapter;
            AlbumsRecyclerView.SetAdapter(AlbumsAdapter);
            AlbumsRecyclerView.NestedScrollingEnabled = false;

            //ARTIST RV
            ArtistsLayoutManager = new LinearLayoutManager(this.Activity);
            ArtistsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.artists_rv);
            ArtistsRecyclerView.SetLayoutManager(ArtistsLayoutManager);
            ArtistsAdapter = new BoxedRV(Artists, ArtistsRecyclerView, this.Context);
            Artists.Adapter = ArtistsAdapter;
            ArtistsRecyclerView.SetAdapter(ArtistsAdapter);
            ArtistsRecyclerView.NestedScrollingEnabled = false;

            ArtistsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (ArtistsRecyclerView != null && ArtistsRecyclerView.ChildCount != 0)
                {
                    //Current_state.SetSong(Current_state.Current_Song_List[position]);
                }
            });

            Task.Run(() => Checker());

            return RootView;
        }

        private void Search_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (search.IsFocused)
            {
                if (search.Text.Contains("Search song, album, playlist"))
                    search.Text = "";
            }

            if (!SongFinded && !AlbumFinded && !ArtistFinded && !PlaylistFinded)
                IsSearchResultEmpty = true;
            else
                IsSearchResultEmpty = false;
        }

        private void Search_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
        }

        public async Task Checker()
        {
            var query = "";
            while (true)
            {
                try
                {
                    if (query != search.Text)
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            Songs.clear();
                            Albums.clear();
                            Artists.clear();
                        }, null);
                        await Task.Delay(500);
                        var a = Task.Run(() => SearchSong(search.Text));
                        var b = Task.Run(() => SearchAlbums(search.Text));
                        var c = Task.Run(() => SearchArtist(search.Text));

                        while (!(a.IsCompletedSuccessfully || a.IsCanceled || a.IsCompleted || a.IsFaulted)) await Task.Delay(500);
                        while (!(b.IsCompletedSuccessfully || b.IsCanceled || b.IsCompleted || b.IsFaulted)) await Task.Delay(500);
                        while (!(c.IsCompletedSuccessfully || c.IsCanceled || c.IsCompleted || c.IsFaulted)) await Task.Delay(500);

                        if (!SongFinded && !AlbumFinded && !ArtistFinded && !PlaylistFinded)
                            SearchEmpty.Post(() => SearchEmpty.Visibility = ViewStates.Visible);
                        else
                            SearchEmpty.Post(() => SearchEmpty.Visibility = ViewStates.Gone);

                        query = search.Text;
                    }
                }
                catch (Exception e)
                {
                    Snackbar.Make(this.Activity.Window.DecorView.RootView, e.Message, Snackbar.LengthShort).Show();
                }
                await Task.Delay(500);
            }
        }

        public async Task SearchSong(string query)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/api/songs/search");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(query), ParameterType.RequestBody);
                IRestResponse response = await Client.ExecuteTaskAsync(request);
                if (response.IsSuccessful)
                {
                    var Songsx = JsonConvert.DeserializeObject<List<Item>>(response.Content);
                    if (Songsx != null && Songsx.Count != 0)
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            SongFinded = true;
                            if (SongsContainer.Visibility == ViewStates.Gone)
                                SongsContainer.Visibility = ViewStates.Visible;
                        }, null);

                        foreach (var x in Songsx.Take(8))
                        {
                            Songs.Add(new List(x.Id, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name));
                        }
                    }
                    else
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            SongFinded = false;
                            if (SongsContainer.Visibility == ViewStates.Visible)
                                SongsContainer.Visibility = ViewStates.Gone;
                        }, null);
                    }
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                Application.SynchronizationContext.Post(_ =>
                {
                    SongFinded = false;
                    if (SongsContainer.Visibility == ViewStates.Visible)
                        SongsContainer.Visibility = ViewStates.Gone;
                }, null);
            }
        }

        public async Task SearchAlbums(string query)
        {

            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/Api/Album/Search");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(query), ParameterType.RequestBody);
                IRestResponse response = await Client.ExecuteTaskAsync(request);
                if (response.IsSuccessful)
                {
                    var Albumsx = JsonConvert.DeserializeObject<List<Album>>(response.Content);
                    if (Albumsx != null && Albumsx.Count != 0)
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            AlbumFinded = true;
                            if (AlbumsContainer.Visibility == ViewStates.Gone)
                                AlbumsContainer.Visibility = ViewStates.Visible;
                        }, null);
                        for (int i = 0; i < Albumsx.Count && i <= 8; i = i + 2)
                        {
                            if (Albums.Count - i == 1)
                            {
                                var x = Albumsx[i];
                                Albums.Add(new TwoBlockWithImage(
                                new BlockWithImage(
                                    x.Id,
                                    RvType.Album,
                                    x.Name,
                                    x.Label,
                                    x.Images.First().Url)));
                            }
                            else
                            {
                                var x = Albumsx[i];
                                var y = Albumsx[i + 1];
                                Albums.Add(new TwoBlockWithImage(
                                    new BlockWithImage(
                                        x.Id,
                                        RvType.Album,
                                        x.Name,
                                        x.Label,
                                        x.Images.First().Url),
                                        new BlockWithImage(
                                        y.Id,
                                        RvType.Album,
                                        y.Name,
                                        x.Label,
                                        y.Images.First().Url)));
                            }
                        }
                    }
                    else
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            AlbumFinded = false;
                            if (AlbumsContainer.Visibility == ViewStates.Visible)
                                AlbumsContainer.Visibility = ViewStates.Gone;
                        }, null);
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {
                Application.SynchronizationContext.Post(_ =>
                {
                    AlbumFinded = false;
                    if (AlbumsContainer.Visibility == ViewStates.Visible)
                        AlbumsContainer.Visibility = ViewStates.Gone;
                }, null);
            }
        }

        public async Task SearchArtist(string query)
        {

            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/Api/artist/Search");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(query), ParameterType.RequestBody);
                IRestResponse response = await Client.ExecuteTaskAsync(request);
                if (response.IsSuccessful)
                {
                    var mArtists = JsonConvert.DeserializeObject<List<Artist>>(response.Content);
                    if (mArtists != null && mArtists.Count != 0)
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            ArtistFinded = true;
                            if (ArtistContainer.Visibility == ViewStates.Gone)
                                ArtistContainer.Visibility = ViewStates.Visible;
                        }, null);
                        for (int i = 0; i < mArtists.Count && i <= 8; i = i + 2)
                        {
                            if (mArtists.Count - i == 1)
                            {
                                var x = mArtists[i];
                                Artists.Add(new TwoBlockWithImage(
                                new BlockWithImage(
                                    x.Id,
                                    RvType.Album,
                                    x.Name,
                                    x.Genres != null ? JsonConvert.DeserializeObject<List<string>>(x.Genres).First() : string.Empty,
                                    x.Images.First().Url)));
                            }
                            else
                            {
                                var x = mArtists[i];
                                var y = mArtists[i + 1];
                                Albums.Add(new TwoBlockWithImage(
                                    new BlockWithImage(
                                        x.Id,
                                        RvType.Album,
                                        x.Name,
                                        x.Genres != null ? JsonConvert.DeserializeObject<List<string>>(x.Genres).First() : string.Empty,
                                        x.Images.First().Url),
                                        new BlockWithImage(
                                        y.Id,
                                        RvType.Album,
                                        y.Name,
                                        x.Genres != null ? JsonConvert.DeserializeObject<List<string>>(x.Genres).First() : string.Empty,
                                        y.Images.First().Url)));
                            }
                        }
                    }
                    else
                    {
                        Application.SynchronizationContext.Post(_ =>
                        {
                            ArtistFinded = false;
                            if (ArtistContainer.Visibility == ViewStates.Visible)
                                ArtistContainer.Visibility = ViewStates.Gone;
                        }, null);
                    }
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                Application.SynchronizationContext.Post(_ =>
                {
                    ArtistFinded = false;
                    if (ArtistContainer.Visibility == ViewStates.Visible)
                        ArtistContainer.Visibility = ViewStates.Gone;
                }, null);
            }
        }
    }
}