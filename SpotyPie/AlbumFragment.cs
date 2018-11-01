using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Constraints;
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
using System.Linq;
using System.Threading.Tasks;
using static Android.Views.View;
using static Android.Views.ViewGroup;
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

        private TextView download;
        private ConstraintLayout backViewContainer;
        int Height = 0;

        MarginLayoutParams MarginParrams;
        RelativeLayout relative;

        private NestedScrollView Scroll;
        private NestedScrollView ScrollFather;
        int scrolled = 0;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.Album_layout, container, false);

            download = RootView.FindViewById<TextView>(Resource.Id.textView4);

            MarginParrams = (MarginLayoutParams)download.LayoutParameters;

            relative = RootView.FindViewById<RelativeLayout>(Resource.Id.hide);

            ScrollFather = RootView.FindViewById<NestedScrollView>(Resource.Id.fatherScrool);
            //ScrollFather.SetOnTouchListener(this);
            backViewContainer = RootView.FindViewById<ConstraintLayout>(Resource.Id.backViewContainer);
            Height = backViewContainer.LayoutParameters.Height;
            ScrollFather.ScrollChange += Scroll_ScrollChange;

            //RECENT ALBUMS
            AlbumSongsLayoutManager = new LinearLayoutManager(this.Activity);
            AlbumSongsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.song_list);
            AlbumSongsRecyclerView.SetLayoutManager(AlbumSongsLayoutManager);
            AlbumSongsAdapter = new VerticalRV(AlbumSongs, AlbumSongsRecyclerView, this.Context);
            AlbumSongs.Adapter = AlbumSongsAdapter;
            AlbumSongsRecyclerView.SetAdapter(AlbumSongsAdapter);
            AlbumSongsRecyclerView.NestedScrollingEnabled = false;

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
        private void Scroll_ScrollChange(object sender, NestedScrollView.ScrollChangeEventArgs e)
        {
            scrolled = ScrollFather.ScrollY;
            MainActivity.ActionName.Text = ((int)scrolled).ToString();
            if (scrolled < Height) //761 mazdaug
            {
                relative.Visibility = ViewStates.Invisible;
                //MarginParrams.TopMargin = Height - scrolled;

                //download.LayoutParameters = MarginParrams;
                //AlbumSongsRecyclerView.NestedScrollingEnabled = false;
            }
            else
            {
                relative.Visibility = ViewStates.Visible;
                //RecycleViewList has to scrool inside
                AlbumSongsRecyclerView.NestedScrollingEnabled = true;

            }
        }
    }
}