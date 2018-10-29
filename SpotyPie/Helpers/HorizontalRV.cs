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

namespace SpotyPie.Helpers
{
    public class HorizontalRV : RecyclerView.Adapter
    {
        private RecycleViewList<string> Paskaitos;
        private readonly RecyclerView mRecyclerView;

        public HorizontalRV(RecycleViewList<string> paskaitos, RecyclerView recyclerView)
        {
            Paskaitos = paskaitos;
            mRecyclerView = recyclerView;
        }

        public class Loading : RecyclerView.ViewHolder
        {
            public View LoadingView { get; set; }

            public Loading(View view) : base(view)
            { }
        }

        public class EmptyTime : RecyclerView.ViewHolder
        {
            public View EmptyTimeView { get; set; }
            public EmptyTime(View view) : base(view) { }
        }

        public override int GetItemViewType(int position)
        {
            if (Paskaitos[position] == null)
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

                EmptyTime view = new EmptyTime(EmptyTime) { };

                return view;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is Loading)
            {
                ;
            }
            else if (holder is EmptyTime)
            {
                ;
            }
        }

        public override int ItemCount
        {
            get { return Paskaitos.Count; }
        }
    }
}