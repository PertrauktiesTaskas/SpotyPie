﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SpotyPie.Models;
using Square.Picasso;

namespace SpotyPie.Helpers
{
    public class VerticalRV : RecyclerView.Adapter
    {
        private RecycleViewList<Item> Dataset;
        private Context Context;

        public VerticalRV(RecycleViewList<Item> data, Context context)
        {
            Dataset = data;
            Context = context;
        }

        public class Loading : RecyclerView.ViewHolder
        {
            public View LoadingView { get; set; }

            public Loading(View view) : base(view)
            { }
        }

        public class BlockImage : RecyclerView.ViewHolder
        {
            public View EmptyTimeView { get; set; }

            public TextView Title { get; set; }

            public TextView SubTitile { get; set; }

            public ImageButton Options { get; set; }

            public BlockImage(View view) : base(view) { }
        }

        public override int GetItemViewType(int position)
        {
            if (Dataset[position] == null)
            {
                return Resource.Layout.Loading;
            }
            else
            {
                return Resource.Layout.song_list_rv;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == Resource.Layout.Loading)
            {
                View Loading = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Loading, parent, false);

                Loading view = new Loading(Loading) { };

                return view;
            }
            else
            {
                View EmptyTime = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.song_list_rv, parent, false);

                TextView mTitle = EmptyTime.FindViewById<TextView>(Resource.Id.Title);
                TextView mSubTitle = EmptyTime.FindViewById<TextView>(Resource.Id.subtitle);
                ImageButton mImage = EmptyTime.FindViewById<ImageButton>(Resource.Id.option);

                BlockImage view = new BlockImage(EmptyTime)
                {
                    Title = mTitle,
                    SubTitile = mSubTitle,
                    Options = mImage,
                    IsRecyclable = false
                };

                return view;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is Loading)
            {
                ;
            }
            else if (holder is BlockImage)
            {
                BlockImage view = holder as BlockImage;
                view.Title.Text = Dataset[position].Name;
                view.SubTitile.Text = JsonConvert.DeserializeObject<List<Artist>>(Dataset[position].Artists).First().Name;
                view.Options.Click += Options_Click;
                MainActivity.Add_to_playlist_id = Dataset[position].Id;
            }
        }

        private void Options_Click(object sender, EventArgs e)
        {
            MainActivity.LoadOptionsMeniu();
        }

        public override int ItemCount
        {
            get { return Dataset.Count; }
        }
    }
}