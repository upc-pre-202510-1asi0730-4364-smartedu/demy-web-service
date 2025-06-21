namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;

public record TimeRange
{
    public TimeOnly StartTime { get; init; }
    public TimeOnly EndTime { get; init; }

    public TimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
            throw new ArgumentException("Start time must be before end time");
            
        StartTime = startTime;
        EndTime = endTime;
    }

    public TimeRange(string startTime, string endTime)
        : this(TimeOnly.Parse(startTime), TimeOnly.Parse(endTime))
    {
    }

    /// <summary>
    /// Gets the duration of the time range in minutes
    /// </summary>
    public int DurationInMinutes => (int)(EndTime - StartTime).TotalMinutes;

    /// <summary>
    /// Checks if this time range overlaps with another time range
    /// </summary>
    public bool OverlapsWith(TimeRange other)
    {
        return StartTime < other.EndTime && EndTime > other.StartTime;
    }
}