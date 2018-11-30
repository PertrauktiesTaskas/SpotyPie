using Android.App;
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
    public class Search : SupportFragment
    {
        View RootView;

        //List Songs
        public static List<Item> SearchSongs = new List<Item>();
        public static RecycleViewList<List> Songs = new RecycleViewList<List>();
        private RecyclerView.LayoutManager AlbumSongsLayoutManager;
        private static RecyclerView.Adapter AlbumSongsAdapter;
        private static RecyclerView AlbumSongsRecyclerView;

        public static RecycleViewList<TwoBlockWithImage> Albums = new RecycleViewList<TwoBlockWithImage>();
        private RecyclerView.LayoutManager AlbumsLayoutManager;
        private static RecyclerView.Adapter AlbumsAdapter;
        private static RecyclerView AlbumsRecyclerView;

        EditText search;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.search_layout, container, false);
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
        }

        private void Search_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (search.Text.Length > 3)
            {
            }
            else
            {
                Search.Songs.clear();
                Search.Albums.clear();
            }
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
                        Search.Songs.clear();
                        Search.Albums.clear();
                        //if (!search.IsFocused) break;
                        var a = Task.Run(() => SearchSong(search.Text));
                        var b = Task.Run(() => SearchAlbums(search.Text));
                        //if (!search.IsFocused) break;

                        while (!(a.IsCompletedSuccessfully || a.IsCanceled || a.IsCompleted || a.IsFaulted))
                        {
                            //if (!search.IsFocused) break;
                            await Task.Delay(100);
                        }
                        while (!(b.IsCompletedSuccessfully || b.IsCanceled || b.IsCompleted || b.IsFaulted))
                        {
                            //if (!search.IsFocused) break;
                            await Task.Delay(100);
                        }
                        //if (!search.IsFocused) break;
                        query = search.Text;
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                }
                catch (Exception e)
                {

                }
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
                    var Songs = JsonConvert.DeserializeObject<List<Item>>(response.Content);
                    if (Songs != null && Songs.Count != 0)
                    {
                        foreach (var x in Songs.Take(8))
                        {
                            Search.Songs.Add(new List(x.Id, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name));
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception e)
            {

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
                    var Albums = JsonConvert.DeserializeObject<List<Album>>(response.Content);
                    if (Albums != null && Albums.Count != 0)
                    {
                        for (int i = 0; i < Albums.Count && i <= 8; i = i + 2)
                        {
                            if (Albums.Count - i == 1)
                            {
                                var x = Albums[i];
                                Search.Albums.Add(new TwoBlockWithImage(
                                new BlockWithImage(
                                    x.Id,
                                    RvType.Album,
                                    x.Name,
                                    x.Label,
                                    x.Images.First().Url)));
                            }
                            else
                            {
                                var x = Albums[i];
                                var y = Albums[i + 1];
                                Search.Albums.Add(new TwoBlockWithImage(
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
                }
                else
                {

                }
            }
            catch (Exception)
            {

            }
        }
    }
}