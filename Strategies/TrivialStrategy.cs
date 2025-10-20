
namespace MiniDeepThought.Strategies;

internal class TrivialStrategy : IAnswerStrategy
{
    public Task<string> AnswerQuestion(string question, IProgress<int>? progress = null)
    {
        return Task.FromResult("42");
    }
}
