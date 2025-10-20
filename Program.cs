using MiniDeepThought.Util;

do
{
    int option = ConsoleHelpers.OptionSelector();
    switch (option)
    {
        case 1:
            ConsoleHelpers.SubmitQuestion();
            break;
        case 2:
            ConsoleHelpers.ListJobs();
            break;
        case 3:
            ConsoleHelpers.ViewResultByJobId();
            break;
        case 4:
            ConsoleHelpers.CancelJob();
            break;
        case 5:
            return;
        default:
            break;

    }


} while (true);