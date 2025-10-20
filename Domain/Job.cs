using MiniDeepThought.Domain;
using MiniDeepThought.Strategies;
using System.Diagnostics;

namespace MiniDeepThought.Domanin;

internal class Job
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public JobStatus Status { get; set; } = JobStatus.None;
    public string QuestionText { get; init; } = string.Empty;
    public IAnswerStrategy Algorithm { get; init; }
    public Stopwatch? Stopwatch { get; set; }   
    public TimeSpan? Duration { get; set; }
    public DateTime CreatedUtc { get; init; } = DateTime.Now;
    public string Progress { get; set; } = "0%";
    public string Result { get; set; } = string.Empty; 

    public Job(string questionText, IAnswerStrategy algorithm)
    {
        QuestionText = questionText;
        Algorithm = algorithm;
        CreatedUtc = DateTime.UtcNow;
        Stopwatch = new Stopwatch();
    }
    public void UpdatedProgress(int progress) => Progress = $"{progress}%";
}
