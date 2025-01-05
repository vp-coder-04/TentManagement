using AutoMapper;
using TentBooking.Features.Dtos;
using TentBooking.Models;

namespace TentBooking.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Booking, TentBookingDto>();
        }
    }
}
