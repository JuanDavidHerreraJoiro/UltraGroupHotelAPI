using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Features.Bookings.Commands.CreateBooking;
using UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookingDynamicFilter;
using UltraGroupHotelAPI.Application.Features.Bookings.Queries.GetBookinsList;
using UltraGroupHotelAPI.Application.Features.Cities.Queries.GetCitiesList;
using UltraGroupHotelAPI.Application.Features.DocumentTypes.Queries;
using UltraGroupHotelAPI.Application.Features.Genders.Commands.CreateGender;
using UltraGroupHotelAPI.Application.Features.Genders.Queries.GetGendersList;
using UltraGroupHotelAPI.Application.Features.Hotels.Commands.CreateHotel;
using UltraGroupHotelAPI.Application.Features.Hotels.Commands.UpdateHotel;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelRoomsEnabledList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsEnabledList;
using UltraGroupHotelAPI.Application.Features.Hotels.Queries.GetHotelsList;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom;
using UltraGroupHotelAPI.Application.Features.Rooms.Commands.UpdateRoom;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomAvailableList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomEnabledList;
using UltraGroupHotelAPI.Application.Features.Rooms.Queries.GetRoomList;
using UltraGroupHotelAPI.Application.Features.RoomTypes.Queries.GetRoomTypesList;
using UltraGroupHotelAPI.Application.Features.Travellers.Commands.CreateTraveller;
using UltraGroupHotelAPI.Application.Features.Travellers.Queries.GetTravellersList;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            //Gender
            CreateMap<Gender, GenderVm>();
            CreateMap<CreateGenderCommand, Gender>();

            //RoomType
            CreateMap<RoomType, RoomTypeVm>();

            //RoomType
            CreateMap<DocumentType, DocumentTypeVm>();

            //RoomType
            CreateMap<City, CityVm>();

            //Hotel
            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<UpdateHotelCommand, Hotel>();
            CreateMap<Hotel, HotelVm>();
            CreateMap<Hotel, HotelEnabledVm>();
            CreateMap<Hotel, HotelRoomsEnabledVm>();

            //Room
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<Room, RoomVm>();
            CreateMap<Room, RoomEnabledVm>();
            CreateMap<Room, RoomAvailableVm>();
            CreateMap<UpdateRoomCommand, Room>();

            //Traveler
            CreateMap<Traveler, TravelerVm>();
            CreateMap<CreateTravelerCommand, Traveler>();

            //Booking
            CreateMap<CreateBookingCommand, Booking>();
            CreateMap<Booking, BookingVm>();
            CreateMap<Booking, BookingDynamicFilterVm>();
        }

    }
}
