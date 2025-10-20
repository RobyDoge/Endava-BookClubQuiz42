using MiniDeepThought.Domain;
using MiniDeepThought.Domanin;
using MiniDeepThought.Strategies;
using System.Collections.Concurrent;

namespace MiniDeepThought.Services;

internal class JobStore
{
    private ConcurrentDictionary<Guid, Job> Jobs = new();

    public Job CreateJob(string question, IAnswerStrategy strategy)
    {
        var job = new Job(question, strategy);
        job.Status = JobStatus.Queued;
        Jobs[job.Id] = job;
        return job;
    }

    public IEnumerable<Job> GetAllJobs()
    {
        return Jobs.Values;
    }

    public void MarkRunning(Guid jobId)
    {
        if (Jobs.TryGetValue(jobId, out var job))
        {
            job.Status = JobStatus.Running;
            job.Stopwatch?.Start();
        }
    }

    public void UpdateProgress(Guid jobId, int progress)
    {
        if (Jobs.TryGetValue(jobId, out var job))
        {
            job.UpdatedProgress(progress);
        }
    }

    public string GetQuestionText(Guid jobId)
    {
        if (Jobs.TryGetValue(jobId, out var job))
        {
            return job.QuestionText;
        }
        throw new KeyNotFoundException("Job not found");
    }
    public void MarkComplete(Guid jobId,string answer)
    {
        if (Jobs.TryGetValue(jobId, out var job))
        {
            job.Status = JobStatus.Completed;
            job.Stopwatch?.Stop();
            job.Duration = job.Stopwatch?.Elapsed;
            job.UpdatedProgress(100);
            job.Result = answer;
        }
    }

    public void MarkFail(Guid jobId)
    {
        if (Jobs.TryGetValue(jobId, out var job))
        {
            job.Status = JobStatus.Failed;
            job.Stopwatch?.Stop();
            job.Duration = job.Stopwatch?.Elapsed;
        }
    }

    public void MarkCancelled(Guid jobId)
    {
        if (Jobs.TryGetValue(jobId, out var job))
        {
            job.Status = JobStatus.Canceled;
            job.Stopwatch?.Stop();
            job.Duration = job.Stopwatch?.Elapsed;
        }
    }
}
