using MediatR;
using TentBooking.Features.Dtos;

namespace TentBooking.Features.Query
{
    public class GetTentBookingQuery : IRequest<IEnumerable<TentBookingDto>>
    {
    }
}
