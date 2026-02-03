using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MvcMusicStore.Application.Common.Interfaces;
using MvcMusicStore.Application.Checkout.Commands;
using MvcMusicStore.Application.Checkout.Queries;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CheckoutController : Controller
    {
        private readonly IMediator _mediator;
        private readonly MusicStoreEntities _context;
        private readonly ILogger<CheckoutController> _logger;
        public CheckoutController(MusicStoreEntities context, ILogger<CheckoutController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        const string PromoCode = "FREE";
        [HttpGet]
        //
        // GET: /Checkout/AddressAndPayment
        public IActionResult AddressAndPayment()
        {
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<IActionResult> AddressAndPayment([FromBody] IFormCollection values)
        {
            var result = await _mediator.Send(new CheckoutAddressAndPaymentCommand { Values = values });
            return result.IsSuccess ? Ok() : BadRequest();
        }
        [HttpGet("{id:int}")]
        //
        // GET: /Checkout/Complete
        public async Task<IActionResult> Complete([FromRoute] int id)
        {
            var result = await _mediator.Send(new CheckoutCompleteQuery { Id = id });
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}