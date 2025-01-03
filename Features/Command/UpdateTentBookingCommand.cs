using MediatR;

namespace TentBooking.Features.Command
{
    public class UpdateTentBookingCommand : IRequest<bool>
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
