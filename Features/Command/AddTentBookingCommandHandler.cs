using MediatR;
using TentBooking.Models;
using TentBooking.Services;

namespace TentBooking.Features.Command
{
    public class AddTentBookingCommandHandler : IRequestHandler<AddTentBookingCommand, int>
    {
        private readonly IBaseRepository<Booking> _baseRepository;
        public AddTentBookingCommandHandler(IBaseRepository<Booking> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<int> Handle(AddTentBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                BookingDate = request.BookingDate,
                TeamId = request.TeamId,
                ProductId = request.ProductId,
                DesignId = request.DesignId,
                FunctionName = request.FunctionName,
                Price = request.Price
            };
            await _baseRepository.AddAsync(booking); // Assume AddAsync is implemented in your repository.
            return booking.BookingId;
        }
    }
}
