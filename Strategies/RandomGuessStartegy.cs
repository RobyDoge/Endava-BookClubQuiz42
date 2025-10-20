namespace MiniDeepThought.Strategies;

internal class RandomGuessStartegy: IAnswerStrategy
{
    private Random RandomSeed { get; init; } = new Random();
    private string[] PossibleAnswers { get; init; } = ["42"];

    public async Task<string> AnswerQuestion(string question, IProgress<int>? progress = null)
    {
        await Task.Delay(800);
        return PossibleAnswers[RandomSeed.Next(PossibleAnswers.Length)];
    }
}
