using MiniDeepThought.Domain;
using MiniDeepThought.Domanin;
using System.Collections.Concurrent;

namespace MiniDeepThought.Services;

internal class JobStore
{
    private ConcurrentDictionary<Guid, Job> Jobs = new();

    public Job Create(Job job)
    {
        var job = new Job()
        return job;
    }
}
