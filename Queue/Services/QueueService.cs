using Queue.Controllers;

namespace Queue.Services;

public class QueueService : IDisposable
{
    public readonly Queue<Session> _queue = new();

    public void Enqueue(Session session)
    {
        if (!_queue.Contains(session))
        {
            _queue.Enqueue(session);
        }
    }
    
    public Session? Dequeue()
    {
        if (_queue.Count > 0)
            return _queue.Dequeue();
        else
            return null;
    }

    public int GetSpot(Guid id)
    {
        return _queue.ToList().FindIndex(x => x.Id == id);
    }

    public bool Contains(Session session)
    {
        return GetSpot(session.Id) > -1;
    }

    public void Dispose()
    {
        //
    }
}