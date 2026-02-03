using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Application.Store.Queries;
using System.Threading.Tasks;

namespace MvcMusicStore.Controllers
{
    [Route("[controller]")]
    public class StoreController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MusicStoreEntities _context;
        private readonly ILogger<StoreController> _logger;
        public StoreController(MusicStoreEntities context, ILogger<StoreController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        //
        // GET: /Store/
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new StoreGetListQuery());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet]
        //
        // GET: /Store/Browse?genre=Disco
        public async Task<IActionResult> Browse(string genre)
        {
            var result = await _mediator.Send(new StoreBrowseQuery { Genre = genre });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet("{id:int}")]
        //
        // GET: /Store/Details/5
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var result = await _mediator.Send(new StoreGetByIdQuery { Id = id });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet]
        // TODO: [ChildActionOnly] removed - convert to ViewComponent for ASP.NET Core
        public async Task<IActionResult> GenreMenu()
        {
            var result = await _mediator.Send(new StoreGenreMenuQuery());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
    }
}