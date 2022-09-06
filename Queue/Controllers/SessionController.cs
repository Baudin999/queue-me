using Microsoft.AspNetCore.Mvc;
using Queue.Services;

namespace Queue.Controllers;

[ApiController]
public class SessionController
{
    private readonly QueueService _queueService;
    private readonly SessionService _sessionService;

    public SessionController(QueueService queueService, SessionService sessionService)
    {
        _queueService = queueService;
        _sessionService = sessionService;
    }
    
    
    [HttpGet("/session")]
    public bool HasActiveSession(Guid id)
    {
        return _sessionService._sessions.ContainsKey(id);
    }


    [HttpPost("/session")]
    public Session RequestSession(Guid id)
    {
        _sessionService._sessions.TryGetValue(id, out var session);
        if (session != null)
        {
            session.TTL = DateTime.Now.Add(TimeSpan.FromMinutes(5));
            return session;
        }
        
        
        // if the session is not active, check if there is a free session
        session = new Session(id);
        _queueService.Enqueue(session);

        return session;
    }
    
  
}