using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagementProject.API.Services.Interfaces;
using TicketManagementProject.Shared.DTOs;

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
        public async Task<IActionResult> Create(TicketDto dto)
        {
            await _ticketService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TicketDto dto)
        {
            await _ticketService.UpdateAsync(dto.Id!, dto);
            return Ok(dto);
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
