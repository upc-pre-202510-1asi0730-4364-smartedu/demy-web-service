﻿using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.ACL;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.OutboundServices.ACL
{
    /// <summary>
    /// Service that retrieves data from the Scheduling module.
    /// </summary>
    /// <remarks>Paul Sulca</remarks>
    public class ExternalSchedulingService(ISchedulingsContextFacade schedulingsContextFacade)
    {
        /// <summary>
        /// Gets the ID of a weekly schedule by its name.
        /// </summary>
        /// <param name="name">The name of the weekly schedule</param>
        /// <returns>The ID of the weekly schedule, or 0 if not found</returns>
        public async Task<int> FetchWeeklyScheduleIdByName(string name)
        {
            var weeklyScheduleId = await schedulingsContextFacade.FetchWeeklyScheduleIdByName(name);
            return weeklyScheduleId == 0 ? 0 : weeklyScheduleId;
        }
    }
}