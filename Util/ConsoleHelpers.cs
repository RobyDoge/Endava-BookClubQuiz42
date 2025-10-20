using MiniDeepThought.Services;
using MiniDeepThought.Strategies;
using System.Net.Http.Headers;

namespace MiniDeepThought.Util;

internal static class ConsoleHelpers
{
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
    public static void SubmitQuestion()
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

        var jobRunner = new JobRunner(strategy);
        jobRunner.CreateJob(question);



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
