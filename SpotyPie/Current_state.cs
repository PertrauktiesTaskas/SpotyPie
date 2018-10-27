using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SpotyPie
{
    public static class Current_state
    {
        public static bool IsPlaying = true;

        public static string ArtistName = "July Talk";
        public static string SongTitle = "Johny + Mary";
        public static string AlbumTitle = "Touch";
        public static float Progress = 60;
    }
}