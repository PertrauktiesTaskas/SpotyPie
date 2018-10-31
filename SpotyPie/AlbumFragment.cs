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
    public class AlbumFragment : SupportFragment
    {
        View RootView;

        //Album Songs
        public static RecycleViewList<BlockWithImage> AlbumSongs = new RecycleViewList<BlockWithImage>();
        private RecyclerView.LayoutManager AlbumSongsLayoutManager;
        private static RecyclerView.Adapter AlbumSongsAdapter;
        private static RecyclerView AlbumSongsRecyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.Album_layout, container, false);

            //RECENT ALBUMS
            AlbumSongsLayoutManager = new LinearLayoutManager(this.Activity);
            AlbumSongsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.song_list);
            AlbumSongsRecyclerView.SetLayoutManager(AlbumSongsLayoutManager);
            AlbumSongsAdapter = new VerticalRV(AlbumSongs, AlbumSongsRecyclerView, this.Context);
            AlbumSongs.Adapter = AlbumSongsAdapter;
            AlbumSongsRecyclerView.SetAdapter(AlbumSongsAdapter);

            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());
            AlbumSongs.Add(new BlockWithImage());

            return RootView;
        }
    }
}