using Database;
using System;
using System.IO;

namespace Service
{
    public class Service : IDb
    {
        private readonly SpotyPieIDbContext _ctx;

        public Service(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;
        }

        public static bool OpenFile(string path, out FileStream fs)
        {
            try
            {
                fs = File.OpenRead(path);
                return true;
            }
            catch (Exception ex)
            {
                fs = null;
                return false;
            }
        }
    }
}
