using MediatR;
using TentBooking.Models;

namespace TentBooking.Features.Query
{
    public class GetTentBookingByIdQuery : IRequest<Booking>
    {
        public int BookingId { get; set; }
    }
}
