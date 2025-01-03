using MediatR;
using TentBooking.Models;
using TentBooking.Services;

namespace TentBooking.Features.Command
{
    public class UpdateTentBookingCommandHandler : IRequestHandler<UpdateTentBookingCommand, bool>
    {
        private readonly IBaseRepository<Booking> _repository;
        public UpdateTentBookingCommandHandler(IBaseRepository<Booking> repository) {
            _repository = repository;
        }
        public async Task<bool> Handle(UpdateTentBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _repository.GetByIdAsync(request.BookingId);
            if (booking == null) 
                return false;
            booking.BookingDate = request.BookingDate;
            booking.TeamId = request.TeamId;
            booking.ProductId = request.ProductId;
            booking.DesignId = request.DesignId;
            booking.FunctionName = request.FunctionName;
            booking.Price = request.Price;

            await _repository.UpdateAsync(booking);
            return true;
        }
    }
}
