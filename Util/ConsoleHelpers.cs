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
        Console.WriteLine("Choose the Algorithm: Trivial, SlowCount, or RandomGuess: ");
        string algorithm = Console.ReadLine().Trim().ToLower();
        IAnswerStrategy? strategy = null;
        switch (algorithm)
        {
            case "randomguess":
                strategy = new RandomGuessStartegy();
                break;
            case "slowcount":
                strategy = new SlowCountStrategy();
                break;
            case "trivial":
                strategy = new TrivialStrategy();
                break;
            default:
                Console.WriteLine("Invalid input");
                return;
        }
        var jobRunner = new JobRunner(strategy,jobStore);
        var jobId = jobRunner.CreateJob(question);
        Console.WriteLine($"Job created with ID: {jobId}");
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
        throw new NotImplementedException();
    }
    public static void ViewResultByJobId()
    {
        throw new NotImplementedException();
    }
    public static void CancelJob()
    {
        throw new NotImplementedException();
    }

}
