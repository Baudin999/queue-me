using Microsoft.AspNetCore.Mvc;
using Queue.Services;

namespace Queue.Controllers;


[ApiController]
public class QueueController : IDisposable
{
    private readonly QueueService _queueService;

    public QueueController(QueueService queueService)
    {
        _queueService = queueService;
    }

    [HttpGet("/queue")]
    public int Put([FromBody] Guid id)
    {
        return _queueService.GetSpot(id);
    }

    public void Dispose()
    {
        //
    }
}