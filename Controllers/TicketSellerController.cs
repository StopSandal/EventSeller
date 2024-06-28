using AutoMapper;
using EventSeller.DataLayer.EntitiesDto;
using EventSeller.Services.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketSellerController : ControllerBase
    {
        private readonly ITicketSellerService _ticketSellerService;
        public TicketSellerController(ITicketSellerService ticketSellerService)
        {
            _ticketSellerService = ticketSellerService;
        }
        [Authorize]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelTicketPaymentAsync(long ticketId, long transactionId)
        {
            try
            {
                await _ticketSellerService.CancelTicketPaymentAsync(ticketId, transactionId);
                return Ok("Ticket payment canceled successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmTicketPaymentAsync([FromBody] PaymentConfirmationDTO paymentConfirmationDTO)
        {
            try
            {
                await _ticketSellerService.ConfirmTicketPaymentAsync(User.Identity.Name, paymentConfirmationDTO);
                return Ok("Ticket payment confirmed successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("price/{ticketId}")]
        public async Task<IActionResult> GetFullTicketPriceAsync(long ticketId)
        {
            try
            {
                var ticketPriceInfo = await _ticketSellerService.GetFullTicketPriceAsync(ticketId);
                return Ok(ticketPriceInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("available/{ticketId}")]
        public async Task<IActionResult> IsTicketAvailableForPurchaseByIdAsync(long ticketId)
        {
            try
            {
                var isAvailable = await _ticketSellerService.IsTicketAvailableForPurchaseByIdAsync(ticketId);
                return Ok(isAvailable);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("process")]
        public async Task<IActionResult> ProcessTicketBuyingAsync([FromBody] PurchaseTicketDTO purchaseTicketDTO)
        {
            try
            {
                var paymentConfirmation = await _ticketSellerService.ProcessTicketBuyingAsync(purchaseTicketDTO);
                return Ok(paymentConfirmation);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("return")]
        public async Task<IActionResult> ReturnMoneyForPurchaseAsync(long ticketTransactionId)
        {
            try
            {
                await _ticketSellerService.ReturnMoneyForPurchaseAsync(ticketTransactionId);
                return Ok("Money returned successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
