namespace MiniDeepThought.Domain;

internal enum JobStatus
{
    None,
    Queued,
    Completed,
    Running,
    Canceled,
    Failed
}
