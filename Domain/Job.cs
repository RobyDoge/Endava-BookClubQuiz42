using MiniDeepThought.Domain;

namespace MiniDeepThought.Domanin;

internal class Job
{
    public Guid JobId { get; init; } = Guid.NewGuid();
    public JobStatus Status { get; set; } = JobStatus.None;
    public string QuestionText { get; init; } = string.Empty;
    public string AlgorithmKey { get; init; } = string.Empty;
    public Timer? Duration { get; set; }
    public DateTime CreatedUtc { get; init; } = DateTime.Now;
    public decimal Progress { get; set; } = decimal.Zero;
    public string Result { get; set; } = string.Empty; 

    public Job(string questionText, string algorithmKey)
    {
        QuestionText = questionText;
        AlgorithmKey = algorithmKey;
        CreatedUtc = DateTime.UtcNow;
    }
}
