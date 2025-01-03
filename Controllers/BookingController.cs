﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TentBooking.Features.Command;
using TentBooking.Features.Query;

namespace TentBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpGet]
        public async Task<IActionResult> BookingList()
        {
            var bookingList = await _mediator.Send(new GetTentBookingQuery());
            return Ok(bookingList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var result = await _mediator.Send(new GetTentBookingByIdQuery() { BookingId = id});
            if (result == null)
                return NotFound("Booking not found.");
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] AddTentBookingCommand command)
        {
            var bookingId = await _mediator.Send(command);
            return Ok(bookingId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingById(int id)
        {
            var result = await _mediator.Send(new DeleteTentBookingCommand() { BookingId=id});
            if (string.IsNullOrEmpty(result))
                return BadRequest("Failed to delete booking.");
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] UpdateTentBookingCommand command)
        {
            var isUpdated = await _mediator.Send(command);
            if (!isUpdated)
                return NotFound("Booking not found or could not be updated.");
            return Ok("Booking updated successfully.");
        }
    }
}