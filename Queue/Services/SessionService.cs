using Queue.Controllers;

namespace Queue.Services;

public class SessionService
{
    public readonly Dictionary<Guid, Session> _sessions = new();
}