using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Domain.Classes;

namespace UltraGroupHotelAPI.Application.Features.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest<int>
    {
        public double Cost { get; set; }
        public double BaseAmount { get; set; }
        public double Tax { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEnabled { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
    }
}
