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

namespace SpotyPie.Player
{
    public class player_song_list : SupportFragment
    {
        View RootView;

        //Album Songs
        public static List<Item> AlbumSongsItem = new List<Item>();
        public static RecycleViewList<List> AlbumSongs = new RecycleViewList<List>();
        private RecyclerView.LayoutManager AlbumSongsLayoutManager;
        private static RecyclerView.Adapter AlbumSongsAdapter;
        private static RecyclerView AlbumSongsRecyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.player_song_list, container, false);

            //ALBUM song list
            AlbumSongsLayoutManager = new LinearLayoutManager(this.Activity);
            AlbumSongsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.song_list);
            AlbumSongsRecyclerView.SetLayoutManager(AlbumSongsLayoutManager);
            AlbumSongsAdapter = new VerticalRV(AlbumSongs, AlbumSongsRecyclerView, this.Context);
            AlbumSongs.Adapter = AlbumSongsAdapter;
            AlbumSongsRecyclerView.SetAdapter(AlbumSongsAdapter);
            AlbumSongsRecyclerView.NestedScrollingEnabled = false;

            AlbumSongsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (AlbumSongsRecyclerView != null && AlbumSongsRecyclerView.ChildCount != 0)
                {
                    Current_state.SetSong(Current_state.Current_Song_List[position]);
                    Player.PlayerSongListContainer.TranslationX = MainActivity.widthInDp;
                }
            });

            return RootView;
        }

        public override void OnResume()
        {
            if (AlbumSongs == null || AlbumSongs.Count == 0)
            {
                Task.Run(() => GetSongsAsync(Current_state.ClickedInRVH.Id));
            }
            base.OnResume();
        }

        public async Task GetSongsAsync(int id)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.deveim.com/api/album/" + id + "/tracks");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    Album album = JsonConvert.DeserializeObject<Album>(response.Content);
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Current_state.Current_Song_List = album.Songs;
                        foreach (var x in album.Songs)
                        {
                            AlbumSongs.Add(new List(x.Id, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name));
                        }
                        List<Copyright> Copyright = JsonConvert.DeserializeObject<List<Copyright>>(album.Copyrights);
                        //Copyrights.Text = string.Join("\n", Copyright.Select(x => x.Text));
                    }, null);
                }
                else
                {
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Toast.MakeText(this.Context, "GetSongsAsync API call error", ToastLength.Short).Show();
                    }, null);
                }
            }
            catch (Exception)
            {

            }
        }

        public override void OnDestroyView()
        {
            AlbumSongs = new RecycleViewList<List>();
            base.OnDestroyView();
        }
    }
}