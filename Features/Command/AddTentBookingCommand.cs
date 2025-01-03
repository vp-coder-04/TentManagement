using MediatR;
using TentBooking.Models;

namespace TentBooking.Features.Command
{
    public class AddTentBookingCommand : IRequest<int>
    {
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public int? TeamId { get; set; }

        public int? ProductId { get; set; }

        public int? DesignId { get; set; }

        public string FunctionName { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
