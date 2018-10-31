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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.Album_layout, container, false);
            return RootView;
        }
    }
}