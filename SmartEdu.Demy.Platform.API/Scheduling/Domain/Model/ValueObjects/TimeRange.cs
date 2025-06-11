namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;

public record TimeRange(string Start, string End)
{
    public TimeRange() : this(string.Empty, string.Empty) { }
    
    public string Start { get; init; } = Start ?? throw new ArgumentNullException(nameof(Start));
    public string End { get; init; } = End ?? throw new ArgumentNullException(nameof(End));
    
    public bool OverlapsWith(TimeRange other)
    {
        if (other == null) return false;
        
        return !(string.Compare(End, other.Start, StringComparison.Ordinal) <= 0 || 
                 string.Compare(Start, other.End, StringComparison.Ordinal) >= 0);
    }
    
    public bool IsValid()
    {
        return string.Compare(Start, End, StringComparison.Ordinal) < 0;
    }
}