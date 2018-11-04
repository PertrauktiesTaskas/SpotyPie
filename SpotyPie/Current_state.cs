using SpotyPie.Models;

namespace SpotyPie
{
    public static class Current_state
    {
        public static bool IsPlaying = true;

        public static string ArtistName { get; set; }
        public static string SongTitle { get; set; }
        public static string AlbumTitle { get; set; }
        public static float Progress { get; set; }

        public static Android.Support.V4.App.Fragment BackFragment { get; set; }

        public static BlockWithImage ClickedInRVH { get; set; } = null;

        public static Artist Current_Artist { get; set; } = null;
    }
}