using System;
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
using SpotyPie.Models;
using Square.Picasso;

namespace SpotyPie.Helpers
{
    public class HorizontalRV : RecyclerView.Adapter
    {
        private RecycleViewList<BlockWithImage> Dataset;
        private readonly RecyclerView mRecyclerView;
        private Context Context;

        public HorizontalRV(RecycleViewList<BlockWithImage> data, RecyclerView recyclerView, Context context)
        {
            Dataset = data;
            mRecyclerView = recyclerView;
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

            public ImageView Image { get; set; }

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
                return Resource.Layout.big_rv_list;
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
                View EmptyTime = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.big_rv_list, parent, false);

                TextView mTitle = EmptyTime.FindViewById<TextView>(Resource.Id.textView10);
                TextView mSubTitle = EmptyTime.FindViewById<TextView>(Resource.Id.textView11);
                ImageView mImage = EmptyTime.FindViewById<ImageView>(Resource.Id.imageView5);

                BlockImage view = new BlockImage(EmptyTime)
                {
                    Image = mImage,
                    SubTitile = mSubTitle,
                    Title = mTitle,
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
                view.Title.Text = Dataset[position].Title;
                view.SubTitile.Text = Dataset[position].SubTitle;
                Picasso.With(Context).Load(Dataset[position].Image).Into(view.Image);

            }
        }

        public override int ItemCount
        {
            get { return Dataset.Count; }
        }
    }
}