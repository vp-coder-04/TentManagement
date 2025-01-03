using MediatR;
using TentBooking.Models;
using TentBooking.Services;

namespace TentBooking.Features.Command
{
    public class DeleteTentBookingCommandHandler : IRequestHandler<DeleteTentBookingCommand, string>
    {
        private readonly IBaseRepository<Booking> _baseRepository;
        public DeleteTentBookingCommandHandler(IBaseRepository<Booking> baseRepository) 
        {
            _baseRepository = baseRepository;
        }
        public async Task<string> Handle(DeleteTentBookingCommand request, CancellationToken cancellationToken)
        {
            return await _baseRepository.DeleteAsync(request.BookingId);
        }
    }
}
