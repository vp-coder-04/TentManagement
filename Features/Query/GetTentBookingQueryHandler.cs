using MediatR;
using TentBooking.Models;
using TentBooking.Services;

namespace TentBooking.Features.Query
{
    public class GetTentBookingQueryHandler : IRequestHandler<GetTentBookingQuery, IEnumerable<Booking>>
    {
        private readonly IBaseRepository<Booking> _repository;
        public GetTentBookingQueryHandler(IBaseRepository<Booking> baseRepository)
        {
                _repository = baseRepository;
        }
        public async Task<IEnumerable<Booking>> Handle(GetTentBookingQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetListAsync();
        }
    }
}
