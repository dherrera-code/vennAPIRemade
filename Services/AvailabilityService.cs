using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using vennAPIRemade.Interface.IRepo;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;
using vennAPIRemade.Models.Entity;

namespace vennAPIRemade.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availRepo;
        private readonly IMapper _mapper;
        public AvailabilityService(IAvailabilityRepository availabilityRepository, IMapper mapper)
        {
            _availRepo = availabilityRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserAvailabilityDTO>> AddNewAvailability(int userId, UserAvailabilityDTO[] availabilityDTO)
        {
            var existingEntries = await _availRepo.GetAvailability(userId);

            var lookup = existingEntries.ToDictionary(
                a => (a.Day, a.Hour),
                a => a
            );

            var incomingKeys = availabilityDTO
            .Select(dto => (dto.Day, dto.Hour))
            .ToHashSet();

            foreach( var dto in availabilityDTO)
            {
                var key = (dto.Day, dto.Hour);

                if(lookup.TryGetValue(key, out var entity))
                {
                    entity.StatusId = dto.StatusId;
                }
                else
                {
                    var newEntity = new UserAvailability
                    {
                        UserId = userId,
                        Day = dto.Day,
                        Hour = dto.Hour,
                        StatusId = dto.StatusId
                    };
                    await _availRepo.AddAsync(newEntity);
                }
            }

            foreach (var entity in existingEntries)
            {
                var key = (entity.Day, entity.Hour);

                if(!incomingKeys.Contains(key))
                {
                    await _availRepo.RemoveAsync(entity);
                }
            }
            var success = await _availRepo.SaveAsync();
            if(success) return await GetWeeklyAvailabilityByUserIdAsync(userId);
            throw new DataException("Unable to Save New Availability");
        }

        public async Task<IEnumerable<UserAvailabilityDTO>> GetDailyAvailabilityById(DayOfWeek dayOfWeek, int userId)
        {
            var availabilityList = await _availRepo.GetAvailabilityByDay(dayOfWeek, userId);
            return _mapper.Map<IEnumerable<UserAvailabilityDTO>>(availabilityList);
        }

        public async Task<IEnumerable<UserAvailabilityDTO>> GetWeeklyAvailabilityByUserIdAsync(int userId)
        {
            var availabilityList = await _availRepo.GetWeeklyAvailability(userId);
            return _mapper.Map<IEnumerable<UserAvailabilityDTO>>(availabilityList);
        }
    }
}