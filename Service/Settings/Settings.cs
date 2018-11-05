
using Config.Net;

namespace Service.Settings
{
    public interface ISettings
    {
        [Option(DefaultValue = @"\root\Audio\")]
        string AudioStoragePath { get; set; }

        [Option(DefaultValue = @"\var\www\cache\")]
        string CachePath { get; set; }

        [Option(DefaultValue = true)]
        bool FirstUse { get; set; }

        [Option(DefaultValue = 1000)]
        int StreamQuality { get; set; }
    }
}
