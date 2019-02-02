using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            Application.SynchronizationContext.Post(_ =>
            {
                mItems.Add(item);

                if (Adapter != null)
                {
                    Adapter.NotifyItemInserted(Count);
                }
            }, null);
        }

        public void RemoveLoading()
        {
            Application.SynchronizationContext.Post(_ =>
            {
                if (mItems.Count !=0 && mItems[0] == null)
                {
                    mItems.RemoveAt(0);

                    if (Adapter != null)
                    {
                        Adapter.NotifyItemRemoved(0);
                    }
                    Adapter.NotifyDataSetChanged();
                }
            }, null);
        }

        public T Get(int position)
        {
            return mItems[position];
        }

        private void Remove(int position)
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

        public async Task ClearAsync()
        {
            if (mItems.Count != 0 || Adapter.ItemCount != 0)
            {
                Application.SynchronizationContext.Post(_ =>
                {
                    try
                    {
                        int size = mItems.Count;
                        mItems = new List<T>();
                        Adapter.NotifyItemRangeRemoved(0, size);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Adapter.NotifyDataSetChanged();
                }, null);

                while (mItems.Count != 0 || Adapter.ItemCount != 0)
                    await Task.Delay(50);
            }
        }
    }
}