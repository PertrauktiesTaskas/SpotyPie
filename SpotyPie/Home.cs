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

        public static RecycleViewList<string> RecentAlbums = new RecycleViewList<string>();
        private RecyclerView.LayoutManager mLayoutManager;
        private static RecyclerView.Adapter mAdapter;
        private static RecyclerView mRecyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.home_layout, container, false);

            mLayoutManager = new LinearLayoutManager(this.Activity, LinearLayoutManager.Horizontal, false);
            mRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.recent_rv);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new HorizontalRV(RecentAlbums, mRecyclerView);
            RecentAlbums.Adapter = mAdapter;
            mRecyclerView.SetAdapter(mAdapter);

            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");
            RecentAlbums.Add("das");

            return RootView;
        }
    }
}