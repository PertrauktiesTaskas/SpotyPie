using Database;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly SpotyPieIDbContext _ctx;

        public AlbumController(SpotyPieIDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}
