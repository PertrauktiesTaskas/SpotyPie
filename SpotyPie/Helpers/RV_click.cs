using Android.Support.V7.Widget;
using Android.Views;
using System;

namespace SpotyPie.Helpers
{
    public static class RV_click
    {
        public static void SetItemClickListener(this RecyclerView rv, Action<RecyclerView, int, View> action)
        {
            rv.AddOnChildAttachStateChangeListener(new AttachStateChangeListener(rv, action));
        }
    }

    public class AttachStateChangeListener : Java.Lang.Object, RecyclerView.IOnChildAttachStateChangeListener
    {
        private RecyclerView mRecyclerview;
        private Action<RecyclerView, int, View> mAction;

        public AttachStateChangeListener(RecyclerView rv, Action<RecyclerView, int, View> action) : base()
        {
            mRecyclerview = rv;
            mAction = action;
        }

        public void OnChildViewAttachedToWindow(View view)
        {
            view.Touch += View_Touch;
        }

        private void View_Touch(object sender, View.TouchEventArgs e)
        {
            Search.Action = e;
            RecyclerView.ViewHolder holder = mRecyclerview.GetChildViewHolder(((View)sender));
            mAction.Invoke(mRecyclerview, holder.AdapterPosition, ((View)sender));
        }

        public void OnChildViewDetachedFromWindow(View view)
        {
            view.Touch -= View_Touch;
        }
    }
}