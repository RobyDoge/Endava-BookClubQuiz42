using MiniDeepThought.Services;
using MiniDeepThought.Strategies;
using System.Net.Http.Headers;

namespace MiniDeepThought.Util;

internal static class ConsoleHelpers
{
    public static JobStore jobStore = new JobStore();
    public static int OptionSelector()
    {
        Console.WriteLine();
        Console.WriteLine("""
        Please choice an input:
        1. Submit Question
        2. List Jobs
        3. View Results by JobId
        4. Cancel Running Job
        5. Exit
        """);
        Console.Write("Option: ");
        if (!int.TryParse(Console.ReadLine(), out int result))
        {
            Console.WriteLine("The input must be a number");
            return 0;
        }
        if (result < 1 || result > 5)  Console.WriteLine("Invalid input range.");
        return result;
    }
    public async static void SubmitQuestion()
    {
        Console.WriteLine("Please write a Question between 1 and 200 characters:");
        string question = Console.ReadLine().Trim();
        if (question is null || question.Length == 0 || question.Length > 200)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        var strategy = GetStrategy();
        if (strategy is null) return;

        var jobRunner = new JobRunner(strategy,jobStore);
        var jobId = jobRunner.CreateJob(question);
        Console.WriteLine($"Job created with ID: {jobId}");
        
        //to implement a better way to cancel the job
        Console.WriteLine("Starting Job.");
        await jobRunner.StartJob(jobId);

        if (strategy is not SlowCountStrategy) return;
        Console.WriteLine("To cancel the job Press 'C' or press any other key to get back to the Main Menu.");
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        if (keyInfo.Key == ConsoleKey.C)
        {
            Console.WriteLine("Cancelling Job...");
            await jobRunner.CancelJob(jobId);
        }
    }
    public static void ListJobs()
    {
        Console.WriteLine("Listing all jobs:");
        //to add headers
        foreach (var job in jobStore.GetAllJobs())
        {
            var jobId = string.Format("{0,-36}", job.Id);
            var status = string.Format("{0,-10}", job.Status);
            var progress = string.Format("{0,-4}", job.Progress);
            var duration = string.Format("{0,-10}", job.Duration?.ToString() ?? "N/A");
            Console.WriteLine($"Job ID: {jobId} | Status: {status} | Progress: {progress} | Duration: {duration}");
        }
    }
    public static void ViewResultByJobId()
    {
        Console.WriteLine("Insert the id of job");
        if (!Guid.TryParse(Console.ReadLine().Trim(), out Guid jobId))
        {
            Console.WriteLine("Invalid input");
            return;
        }
        var job = jobStore.GetAllJobs().FirstOrDefault(j => j.Id == jobId);
        if (job is null)
        {
            Console.WriteLine("Job not found");
            return;
        }
        if (job.Status != Domain.JobStatus.Completed)
        {
            Console.WriteLine($"Job is not completed. Current Status: {job.Status}");
            return;
        }
        Console.WriteLine($"Job Result: {job.Result}");
    }
    public static void CancelJob()
    {
        throw new NotImplementedException();
    }

    private static IAnswerStrategy? GetStrategy()
    {
        Console.WriteLine("Choose the Algorithm: Trivial, SlowCount, or RandomGuess: ");
        switch (Console.ReadLine().Trim().ToLower())
        {
            case "randomguess":
                return new RandomGuessStartegy();
            case "slowcount":
                return new SlowCountStrategy();
            case "trivial":
                return new TrivialStrategy();
            default:
                Console.WriteLine("Invalid input");
                return null;
        }
    }
}
