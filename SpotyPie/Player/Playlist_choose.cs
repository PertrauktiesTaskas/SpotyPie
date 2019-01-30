using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;
using SpotyPie.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SupportFragment = Android.Support.V4.App.Fragment;
namespace SpotyPie.Player
{
    public class Playlist_choose : SupportFragment
    {
        View RootView;

        //Album Songs
        public List<Playlist> PlaylistItem = new List<Playlist>();
        public RecycleViewList<Playlist> Playlist = new RecycleViewList<Playlist>();
        private RecyclerView.LayoutManager PlaylistsLayoutManager;
        private RecyclerView.Adapter PlaylistsAdapter;
        private RecyclerView PlaylistRecyclerView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.playlist_list_choose, container, false);

            //ALBUM song list
            PlaylistsLayoutManager = new LinearLayoutManager(this.Activity);
            PlaylistRecyclerView = RootView.FindViewById<RecyclerView>(Resource.Id.list_rv);
            PlaylistRecyclerView.SetLayoutManager(PlaylistsLayoutManager);
            PlaylistsAdapter = new PlayListChoose(Playlist, Context);
            Playlist.Adapter = PlaylistsAdapter;
            PlaylistRecyclerView.SetAdapter(PlaylistsAdapter);
            PlaylistRecyclerView.NestedScrollingEnabled = false;

            PlaylistRecyclerView.SetItemClickListener((rv, position, view) =>
            {
                if (PlaylistRecyclerView != null && PlaylistRecyclerView.ChildCount != 0)
                {
                }
            });

            return RootView;
        }

        public override void OnResume()
        {
            base.OnResume();
            Task.Run(() => LoadPlaylistsAsync());
        }

        public async Task LoadPlaylistsAsync()
        {
            try
            {
                await Playlist.ClearAsync();
                Playlist.Add(null);

                RestClient Client = new RestClient("http://spotypie.pertrauktiestaskas.lt/api/Playlist/playlists");
                var request = new RestRequest(Method.GET);
                IRestResponse response = await Client.ExecuteGetTaskAsync(request);
                if (response.IsSuccessful)
                {
                    var playlists = JsonConvert.DeserializeObject<List<Playlist>>(response.Content);

                    PlaylistItem = playlists;
                    foreach (var x in playlists)
                    {
                        Playlist.Add(x);
                        await Task.Delay(200);
                    }
                    Playlist.RemoveLoading();
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

        public class PlayListChoose : RecyclerView.Adapter
        {
            private RecycleViewList<Playlist> Dataset;
            private Context Context;

            public PlayListChoose(RecycleViewList<Playlist> data, Context context)
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
                    view.SubTitile.Text = "Current song count -" + Dataset[position].Total;
                    view.Options.Visibility = ViewStates.Gone;
                }
            }

            public override int ItemCount
            {
                get { return Dataset.Count; }
            }
        }
    }
}