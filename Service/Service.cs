using Database;
using System;

namespace Service
{
    public class Service : IDb
    {
        private readonly IDbContext _ctx;

        public Service(IDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}
