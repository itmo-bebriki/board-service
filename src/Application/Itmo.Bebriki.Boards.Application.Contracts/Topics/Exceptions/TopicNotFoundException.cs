namespace Itmo.Bebriki.Boards.Application.Contracts.Topics.Exceptions;

public sealed class TopicNotFoundException : Exception
{
    public TopicNotFoundException() { }

    public TopicNotFoundException(string message) : base(message) { }

    public TopicNotFoundException(string message, Exception inner) : base(message, inner) { }
}