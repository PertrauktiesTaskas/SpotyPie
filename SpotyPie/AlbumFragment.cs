using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Constraints;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;
using SpotyPie.Helpers;
using SpotyPie.Models;
using Square.Picasso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Android.Views.View;
using static Android.Views.ViewGroup;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace SpotyPie
{
    public class AlbumFragment : SupportFragment
    {
        View RootView;

        //Background info

        ImageView AlbumPhoto;
        TextView AlbumTitle;
        Button PlayableButton;
        TextView AlbumByText;

        TextView ButtonBackGround;
        TextView ButtonBackGround2;

        //Album Songs
        public static List<Item> AlbumSongsItem = new List<Item>();
        public static RecycleViewList<List> AlbumSongs = new RecycleViewList<List>();
        private RecyclerView.LayoutManager AlbumSongsLayoutManager;
        private static RecyclerView.Adapter AlbumSongsAdapter;
        private static RecyclerView AlbumSongsRecyclerView;

        private TextView download;
        private TextView Copyrights;
        private ConstraintLayout backViewContainer;
        int Height = 0;

        MarginLayoutParams MarginParrams;
        RelativeLayout relative;
        private NestedScrollView ScrollFather;
        int scrolled = 0;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.Album_layout, container, false);

            MainActivity.ActionName.Text = Current_state.ClickedInRVH.Title;

            //Background binding
            AlbumPhoto = RootView.FindViewById<ImageView>(Resource.Id.album_photo);
            AlbumTitle = RootView.FindViewById<TextView>(Resource.Id.album_title);
            PlayableButton = RootView.FindViewById<Button>(Resource.Id.playable_button);
            AlbumByText = RootView.FindViewById<TextView>(Resource.Id.album_by_title);

            ButtonBackGround = RootView.FindViewById<TextView>(Resource.Id.backgroundHalf);
            ButtonBackGround2 = RootView.FindViewById<TextView>(Resource.Id.backgroundHalfInner);

            Picasso.With(Context).Load(Current_state.ClickedInRVH.Image).Resize(300, 300).CenterCrop().Into(AlbumPhoto);
            AlbumTitle.Text = Current_state.ClickedInRVH.Title;
            AlbumByText.Text = Current_state.ClickedInRVH.SubTitle;

            Current_state.ShowHeaderNavigationButtons();

            download = RootView.FindViewById<TextView>(Resource.Id.download_text);
            Copyrights = RootView.FindViewById<TextView>(Resource.Id.copyrights);
            MarginParrams = (MarginLayoutParams)download.LayoutParameters;

            relative = RootView.FindViewById<RelativeLayout>(Resource.Id.hide);

            ScrollFather = RootView.FindViewById<NestedScrollView>(Resource.Id.fatherScrool);
            backViewContainer = RootView.FindViewById<ConstraintLayout>(Resource.Id.backViewContainer);
            Height = backViewContainer.LayoutParameters.Height;
            ScrollFather.ScrollChange += Scroll_ScrollChange;

            //ALBUM song list
            AlbumSongsLayoutManager = new LinearLayoutManager(this.Activity);
            AlbumSongsRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.song_list);
            AlbumSongsRecyclerView.SetLayoutManager(AlbumSongsLayoutManager);
            AlbumSongsAdapter = new VerticalRV(AlbumSongs, AlbumSongsRecyclerView, this.Context);
            AlbumSongs.Adapter = AlbumSongsAdapter;
            AlbumSongsRecyclerView.SetAdapter(AlbumSongsAdapter);
            AlbumSongsRecyclerView.NestedScrollingEnabled = false;

            AlbumSongsRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (AlbumSongsRecyclerView != null && AlbumSongsRecyclerView.ChildCount != 0)
                {
                    Current_state.SetSong(Current_state.Current_Song_List[position]);
                }
            });

            return RootView;
        }

        public override void OnDestroyView()
        {
            AlbumSongs = new RecycleViewList<List>();
            base.OnDestroyView();
        }

        public override void OnResume()
        {
            if (AlbumSongs == null || AlbumSongs.Count == 0)
            {
                Task.Run(() => GetSongsAsync(Current_state.ClickedInRVH.Id));
            }
            base.OnResume();
        }

        public async Task GetSongsAsync(int id)
        {
            try
            {
                RestClient Client = new RestClient("http://spotypie.pertrauktiestaskas.lt/api/album/" + id + "/tracks");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    Album album = JsonConvert.DeserializeObject<Album>(response.Content);
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Current_state.Current_Song_List = album.Songs;
                        foreach (var x in album.Songs)
                        {
                            AlbumSongs.Add(new List(x.Id, x.Name, JsonConvert.DeserializeObject<List<Artist>>(x.Artists).First().Name));
                        }
                        List<Copyright> Copyright = JsonConvert.DeserializeObject<List<Copyright>>(album.Copyrights);
                        Copyrights.Text = string.Join("\n", Copyright.Select(x => x.Text));
                    }, null);
                }
                else
                {
                    Application.SynchronizationContext.Post(_ =>
                    {
                        Toast.MakeText(this.Context, "GetSongsAsync API call error", ToastLength.Short).Show();
                    }, null);
                }
            }
            catch (Exception)
            {

            }
        }

        private void Scroll_ScrollChange(object sender, NestedScrollView.ScrollChangeEventArgs e)
        {
            scrolled = ScrollFather.ScrollY;
            if (scrolled < Height) //761 mazdaug
            {
                MainActivity.ActionName.Alpha = (float)((scrolled * 100) / Height) / 100;
                ButtonBackGround.Alpha = (float)((scrolled * 100) / Height) / 100;
                relative.Visibility = ViewStates.Invisible;
            }
            else
            {
                relative.Visibility = ViewStates.Visible;
            }
        }
    }
}