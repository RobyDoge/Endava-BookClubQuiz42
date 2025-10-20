using MiniDeepThought.Domanin;
using MiniDeepThought.Strategies;

namespace MiniDeepThought.Services;

internal class JobRunner
{
    public static bool JobIsRunning { get; set; } = false;
    private JobStore JS { get; init; } 
    private IAnswerStrategy Strategy { get; set; }


    public JobRunner(IAnswerStrategy strategy, JobStore jobStore)
    {
        Strategy = strategy;
        JS = jobStore;
    }

    internal void CreateJob(string question)
    {
        CurrentJob = new Job(question, Strategy.GetType().Name);
    }
}
