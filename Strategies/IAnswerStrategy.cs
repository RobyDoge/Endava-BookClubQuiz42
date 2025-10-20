namespace MiniDeepThought.Strategies;

internal interface IAnswerStrategy
{
    Task<string> AnswerQuestion(string question, IProgress<int>? progress = null);
}
