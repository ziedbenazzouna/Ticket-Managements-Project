using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementProject.API.Entities;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Services.Interfaces;

namespace TicketManagementProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tickets = await _ticketService.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            await _ticketService.CreateAsync(ticket);
            return CreatedAtAction(nameof(Get), new { id = ticket.Id }, ticket);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Ticket ticket)
        {
            await _ticketService.UpdateAsync(ticket.Id!, ticket);
            return Ok(ticket);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] Dictionary<string, object> updatedFields)
        {
            if (updatedFields == null || !updatedFields.Any())
                return BadRequest("No fields provided for update");

            await _ticketService.PatchAsync(id, updatedFields);

            return Ok(new { Id = id, UpdatedFields = updatedFields });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _ticketService.DeleteAsync(id);
            return Ok(new { Success = true, Message = "Ticket deleted" });
        }
    }
}
