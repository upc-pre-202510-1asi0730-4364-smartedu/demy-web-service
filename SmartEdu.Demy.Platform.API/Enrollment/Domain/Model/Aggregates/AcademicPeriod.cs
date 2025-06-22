using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates
{
    public class AcademicPeriod
    {
        public int Id { get; private set; }

        public string PeriodName { get; private set; }
        
        public DateRange PeriodDuration { get; private set; }

        public bool IsActive { get; private set; }

        protected AcademicPeriod() { }

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

        public AcademicPeriod(CreateAcademicPeriodCommand command)
            : this(
                command.PeriodName,
                command.StartDate,      
                command.EndDate,        
                command.IsActive)       
        { }
        
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