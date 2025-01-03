using MediatR;
using TentBooking.Models;

namespace TentBooking.Features.Query
{
    public class GetTentBookingQuery : IRequest<IEnumerable<Booking>>
    {
    }
}
