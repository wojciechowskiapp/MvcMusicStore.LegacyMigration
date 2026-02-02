using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;
using Microsoft.Extensions.Logging;
using MvcMusicStore.Application.Common.Interfaces;

namespace MvcMusicStore.Controllers
{
    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MusicStoreEntities _context;
        private readonly ILogger<ShoppingCartController> _logger;
        public ShoppingCartController(MusicStoreEntities context, ILogger<ShoppingCartController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        //
        // GET: /ShoppingCart/
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ShoppingCartGetListQuery());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet("{id:int}")]
        //
        // GET: /Store/AddToCart/5
        public async Task<IActionResult> AddToCart([FromRoute] int id)
        {
            var result = await _mediator.Send(new ShoppingCartAddToCartQuery { Id = id });
            return result.IsSuccess ? RedirectToAction("Index") : BadRequest();
        }//
         // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart([FromRoute] int id)
        {
            var result = await _mediator.Send(new ShoppingCartRemoveFromCartCommand { Id = id });
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }//
         // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        [HttpGet]
        public async Task<IActionResult> CartSummary()
        {
            var result = await _mediator.Send(new ShoppingCartCartSummaryQuery());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
    }
}