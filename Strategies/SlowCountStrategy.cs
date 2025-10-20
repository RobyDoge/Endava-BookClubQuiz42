using System;
using System.Threading.Tasks;

namespace MiniDeepThought.Strategies;

internal class SlowCountStrategy : IAnswerStrategy
{
    public async Task<string> AnswerQuestion(string question, IProgress<int>? progress = null)
    {
        const int forCap = 10;

        for (int i = 0; i < forCap; i++)
        {
            int delayMs = Random.Shared.Next(300, 701);
            await Task.Delay(delayMs);

            int percent = ((i + 1) * 100) / forCap;
            progress?.Report(percent);
        }

        return "42";
    }
}
