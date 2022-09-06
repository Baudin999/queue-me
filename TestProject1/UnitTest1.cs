using Queue.Controllers;
using Queue.Services;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public async void Test1()
    {
        using var queueService = new QueueService();
        using var sessionService = new SessionService(queueService);
        var queueController = new QueueController(queueService);
        var sessionController = new SessionController(queueService, sessionService);

        var person1 = Guid.NewGuid();
        var person2 = Guid.NewGuid();
        var person3 = Guid.NewGuid();
        var person4 = Guid.NewGuid();
        var person5 = Guid.NewGuid();
        var person6 = Guid.NewGuid();
        var person7 = Guid.NewGuid();

        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);
        sessionController.RequestSession(person1);

        // need time to enqueue the items
        await Task.Delay(400).ContinueWith(t =>
        {
            Assert.Single(sessionService._sessions);
            Assert.Empty(queueService._queue);
        });

    }
    
    
    [Fact]
    public async void Test2()
    {
        using var queueService = new QueueService();
        using var sessionService = new SessionService(queueService);
        var queueController = new QueueController(queueService);
        var sessionController = new SessionController(queueService, sessionService);

        var person1 = Guid.NewGuid();
        var person2 = Guid.NewGuid();
        var person3 = Guid.NewGuid();
        var person4 = Guid.NewGuid();
        var person5 = Guid.NewGuid();
        var person6 = Guid.NewGuid();
        var person7 = Guid.NewGuid();
        var person8 = Guid.NewGuid();

        sessionController.RequestSession(person1);
        sessionController.RequestSession(person2);
        sessionController.RequestSession(person3);
        sessionController.RequestSession(person4);
        sessionController.RequestSession(person5);
        sessionController.RequestSession(person6);
        sessionController.RequestSession(person7);
        sessionController.RequestSession(person8);

        // need time to enqueue the items
        await Task.Delay(400).ContinueWith(t =>
        {
            Assert.Equal(3, sessionService._sessions.Count);
            Assert.Equal(5, queueService._queue.Count);
            
            Assert.True(sessionService._sessions.ContainsKey(person1));
            Assert.True(sessionService._sessions.ContainsKey(person2));
            Assert.True(sessionService._sessions.ContainsKey(person3));
        });
    }
}