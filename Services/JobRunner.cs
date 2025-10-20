using MiniDeepThought.Domanin;
using MiniDeepThought.Strategies;

namespace MiniDeepThought.Services;

internal class JobRunner
{
    private JobStore JS { get; init; } 
    private IAnswerStrategy Strategy { get; set; }


    public JobRunner(IAnswerStrategy strategy, JobStore jobStore)
    {
        Strategy = strategy;
        JS = jobStore;
    }

    public Guid CreateJob(string question)
    {
        return JS.CreateJob(question, Strategy).Id;
    }

    public async Task StartJob(Guid jobId)
    {
        JS.MarkRunning(jobId);
        try
        {
            Progress<int>? progress = Strategy is SlowCountStrategy?  new Progress<int>(percent => JS.UpdateProgress(jobId, percent)) : null;
            var answer = await Strategy.AnswerQuestion(JS.GetQuestionText(jobId), progress);
            JS.MarkComplete(jobId,answer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error running job {jobId}: {ex.Message}");
            JS.MarkFail(jobId);
        }
    }

    public async Task CancelJob(Guid jobId)
    {
        
    }
}
