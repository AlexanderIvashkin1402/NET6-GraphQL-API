using CommanderGQL.Models;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CommanderGQL.GraphQL.Messaging;

public class PlatformMessageService
{
    private readonly ISubject<PlatformAddedMessage> _messageStream = new ReplaySubject<PlatformAddedMessage>(1);

    public PlatformAddedMessage AddPlatformAddedMessage(Platform platform)
    {
        var message = new PlatformAddedMessage
        {
            Id = platform.Id,
            Name = platform.Name ?? string.Empty
        };
        _messageStream.OnNext(message);
        return message;
    }

    public IObservable<PlatformAddedMessage> GetMessages()
    {
        return _messageStream.AsObservable();
    }
}
