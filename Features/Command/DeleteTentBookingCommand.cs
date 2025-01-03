using MediatR;

namespace TentBooking.Features.Command
{
    public class DeleteTentBookingCommand : IRequest<string>
    {
        public int BookingId { get; set; }
    }
}
