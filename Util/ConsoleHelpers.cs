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
        throw new NotImplementedException();
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
