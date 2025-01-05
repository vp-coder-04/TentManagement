using AutoMapper;
using MediatR;
using TentBooking.Features.Dtos;
using TentBooking.Models;
using TentBooking.Services;

namespace TentBooking.Features.Query
{
    public class GetTentBookingQueryHandler : IRequestHandler<GetTentBookingQuery, IEnumerable<TentBookingDto>>
    {
        private readonly IBaseRepository<Booking> _repository;
        private readonly IMapper _mapper;
        public GetTentBookingQueryHandler(IBaseRepository<Booking> baseRepository,IMapper mapper)
        {
                _repository = baseRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TentBookingDto>> Handle(GetTentBookingQuery request, CancellationToken cancellationToken)
        {
           var bookings =  await _repository.GetListAsync();
            //return bookings.Select(b => new TentBookingDto
            //{
            //    BookingDate = b.BookingDate,
            //    TeamId = b.TeamId,
            //    ProductId = b.ProductId,
            //    DesignId = b.DesignId,
            //    FunctionName = b.FunctionName,
            //    Price = b.Price
            //}).ToList();
            return _mapper.Map<IEnumerable<TentBookingDto>>(bookings);
        }
    }
}
