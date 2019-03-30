using Android.OS;
using Android.Views;
using SupportFragment = Android.Support.V4.App.Fragment;

namespace SpotyPie
{
    public class BrowseFragment : SupportFragment
    {
        View RootView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RootView = inflater.Inflate(Resource.Layout.browse_layout, container, false);

            return RootView;
        }
    }
}