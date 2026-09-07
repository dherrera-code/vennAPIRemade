using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vennAPIRemade.Interface.IService;
using vennAPIRemade.Models.DTO;

namespace vennAPIRemade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserAvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availability;
        public UserAvailabilityController(IAvailabilityService availabilityService)
        {
            _availability = availabilityService;
        }

        [HttpPost("CreateWeeklyAvailabilityByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<UserAvailabilityDTO>>> CreateUserAvailability(int userId, UserAvailabilityDTO[] availability)
        {
            var result = await _availability.AddNewAvailability(userId, availability);

            if(result == null) return BadRequest("Unable to save availability");

            return Ok(result); 
        }

        [HttpGet("GetUserWeeklyAvailabilityByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<UserAvailabilityDTO>>> GetWeeklyAvailability(int userId)
        {
            var result = await _availability.GetWeeklyAvailabilityByUserIdAsync(userId);
            return Ok(result);
        }
        
        [HttpGet("GetUserAvailabilityByDayOfWeek/{dayOfWeek}/{userId}")]
        public async Task<ActionResult<IEnumerable<UserAvailabilityDTO>>>
        GetAvailabilityByDayAndUser(DayOfWeek dayOfWeek, int userId)
        {
            var result = await _availability.GetDailyAvailabilityById(dayOfWeek, userId);
            return Ok(result);
        }

    }
}