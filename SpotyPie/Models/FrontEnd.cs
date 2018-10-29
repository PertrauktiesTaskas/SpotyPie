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

namespace SpotyPie.Models
{
    public class BlockWithImage
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Image { get; set; }

        public BlockWithImage(string title, string subtitle, string url)
        {
            Title = title;
            SubTitle = subtitle;
            Image = url;
        }
    }
}