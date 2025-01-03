using MediatR;
using TentBooking.Models;
using TentBooking.Services;

namespace TentBooking.Features.Query
{
    public class GetTentBookingByIdQueryHandler : IRequestHandler<GetTentBookingByIdQuery,Booking>
    {
        private readonly IBaseRepository<Booking> _repository;
        public GetTentBookingByIdQueryHandler(IBaseRepository<Booking> baseRepository)
        {
            _repository = baseRepository;
        }

        public Task<Booking> Handle(GetTentBookingByIdQuery request, CancellationToken cancellationToken)
        {
           return _repository.GetByIdAsync(request.BookingId);
        }
    }
}
