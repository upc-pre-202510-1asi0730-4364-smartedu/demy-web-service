using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates
{
    /// <summary>
    /// Aggregate root that represents an academic period with a name,
    /// duration and active status.
    /// </summary>
    /// <remarks>Paul Sulca</remarks>
    public class AcademicPeriod
    {
        /// <summary>
        /// Gets the unique identifier for the academic period.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the academic period.
        /// </summary>
        public string PeriodName { get; private set; }

        /// <summary>
        /// Gets the duration of the academic period as a date range.
        /// </summary>
        public DateRange PeriodDuration { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the academic period is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Required by EF Core.
        /// </summary>
        protected AcademicPeriod() { }

        /// <summary>
        /// Creates a new AcademicPeriod instance.
        /// </summary>
        /// <param name="periodName">Name of the period</param>
        /// <param name="startDate">Start date of the period</param>
        /// <param name="endDate">End date of the period</param>
        /// <param name="isActive">Whether the period is active</param>
        public AcademicPeriod(
            string periodName,
            DateTime startDate,
            DateTime endDate,
            bool isActive)
            : this()
        {
            PeriodName     = periodName;
            PeriodDuration = new DateRange(startDate, endDate);
            IsActive       = isActive;
        }

        /// <summary>
        /// Creates a new AcademicPeriod instance from a creation command.
        /// </summary>
        /// <param name="command">The command containing period data</param>
        public AcademicPeriod(CreateAcademicPeriodCommand command)
            : this(
                command.PeriodName,
                command.StartDate,      
                command.EndDate,        
                command.IsActive)       
        { }

        /// <summary>
        /// Updates the academic period's information.
        /// </summary>
        /// <param name="periodName">New name of the period</param>
        /// <param name="startDate">New start date</param>
        /// <param name="endDate">New end date</param>
        /// <param name="isActive">New active status</param>
        /// <returns>The updated AcademicPeriod instance</returns>
        public AcademicPeriod UpdateInformation(
            string periodName,
            DateTime startDate,
            DateTime endDate,
            bool isActive)
        {
            PeriodName     = periodName;
            PeriodDuration = new DateRange(startDate, endDate);
            IsActive       = isActive;
            return this;
        }
    }
}
