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
    public class RecycleViewList<T>
    {
        private List<T> mItems;
        private RecyclerView.Adapter mAdapter;

        public void Erase()
        {
            mItems = new List<T>();
        }

        public RecycleViewList()
        {
            mItems = new List<T>();
        }

        public RecyclerView.Adapter Adapter
        {
            get { return mAdapter; }
            set { mAdapter = value; }
        }

        public void Add(T item)
        {
            mItems.Add(item);

            if (Adapter != null)
            {
                Adapter.NotifyItemInserted(Count);
            }
        }

        public void Remove(int position)
        {
            mItems.RemoveAt(position);

            if (Adapter != null)
            {
                Adapter.NotifyItemRemoved(0);
            }
        }

        public T this[int index]
        {
            get { return mItems[index]; }
            set { mItems[index] = value; }
        }

        public int Count
        {
            get { return mItems.Count; }
        }

        public void clear()
        {
            int size = mItems.Count;
            Erase();
            mAdapter.NotifyItemRangeRemoved(0, size);
        }
    }
}