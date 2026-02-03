using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Application.Home.Queries;
using System.Threading.Tasks;

namespace MvcMusicStore.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MusicStoreEntities _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(MusicStoreEntities context, ILogger<HomeController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new HomeGetListQuery());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
            return _context.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
        }
    }
}