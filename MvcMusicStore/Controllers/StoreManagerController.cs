using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using MvcMusicStore.Application.Common.Interfaces;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("[controller]")]
    public class StoreManagerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MusicStoreEntities _context;
        private readonly ILogger<StoreManagerController> _logger;
        public StoreManagerController(MusicStoreEntities context, ILogger<StoreManagerController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        //
        // GET: /StoreManager/
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new StoreManagerGetListQuery());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpGet("{id:int}")]
        //
        // GET: /StoreManager/Details/5
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var result = await _mediator.Send(new StoreManagerGetByIdQuery { Id = id });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpPost]
        //
        // GET: /StoreManager/Create
        public async Task<IActionResult> Create()
        {
            var result = await _mediator.Send(new StoreManagerCreateCommand());
            return result.IsSuccess ? View(result.Value) : NotFound();
        }//
         // POST: /StoreManager/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Album album)
        {
            var result = await _mediator.Send(new StoreManagerCreateCommand { Album = album });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpPut("{id:int}")]
        //
        // GET: /StoreManager/Edit/5
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var result = await _mediator.Send(new StoreManagerEditCommand { Id = id });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }//
         // POST: /StoreManager/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Album album)
        {
            var result = await _mediator.Send(new StoreManagerEditCommand { Album = album });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }
        [HttpDelete("{id:int}")]
        //
        // GET: /StoreManager/Delete/5
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _mediator.Send(new StoreManagerDeleteCommand { Id = id });
            return result.IsSuccess ? View(result.Value) : NotFound();
        }//
         // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
        {
            var result = await _mediator.Send(new StoreManagerDeleteConfirmedCommand { Id = id });
            return result.IsSuccess ? RedirectToAction("Index") : BadRequest();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
    }
}