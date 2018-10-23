using Database;
using System;

namespace Service
{
    public class Service : IDb
    {
        private readonly SpotyPieIDbContext _ctx;

        public Service(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}
