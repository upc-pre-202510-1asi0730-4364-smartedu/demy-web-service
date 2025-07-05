using System;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects
{
    public record DateRange
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("StartDate must not be after EndDate");
            StartDate = startDate;
            EndDate = endDate;
        }
        
        // Constructor parameterless requerido para EF Core
        private DateRange() { }
    }
}