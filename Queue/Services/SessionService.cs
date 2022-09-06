using Queue.Controllers;

namespace Queue.Services;

public class SessionService : IDisposable
{
    const int _maxSessions = 3;
    const int _enqueueWait = 300;
    const int _sessionClean = 300;
    private bool continueProcess = true;
    
    public readonly Dictionary<Guid, Session> _sessions = new();
    private readonly QueueService _queueService;

    public SessionService(QueueService queueService)
    {
        _queueService = queueService;
        Task.Run(() =>
        {
            EnqueueSessions(Task.CompletedTask);
        });

        Task.Run(() =>
        {
            EndSession(Task.CompletedTask);
        });
    }

    void EnqueueSessions(Task t)
    {
        //
        lock (_sessions)
        {
            Console.WriteLine("EnqueueSessions sessions: " + _sessions.Count);
            Console.WriteLine("EnqueueSessions queue: " + _queueService._queue.Count);
            while (_sessions.Count < _maxSessions)
            {
                if (_sessions.Count < _maxSessions)
                {
                    var s = _queueService.Dequeue();
                    if (s is not null)
                        _sessions.Add(s.Id, s);
                }
            }
        }
        if (continueProcess)
            Task.Delay(_enqueueWait).ContinueWith(EnqueueSessions);
    }
    
    void EndSession(Task t)
    {
        //
        lock (_sessions)
        {
            Console.WriteLine("EndSession sessions: " + _sessions.Count);
            Console.WriteLine("EndSession queue: " + _queueService._queue.Count);
            var removableSessions = new List<Guid>();
            
            var now = DateTime.Now;
            foreach (Session session in _sessions.Values)
            {
                if (now > session.TTL)
                {
                    removableSessions.Add(session.Id);
                }
            }

            foreach (var session in removableSessions)
            {
                _sessions.Remove(session);
            }
        }
        if (continueProcess)
            Task.Delay(_sessionClean).ContinueWith(EnqueueSessions);
    }

    public void Dispose()
    {
        continueProcess = false;
    }
}