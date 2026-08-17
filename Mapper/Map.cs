using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Mapper
{
    public class Map: Profile
    {
        public Map()
        {
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<RoomEntity, RoomDTO>().ReverseMap();
        }
    }
}