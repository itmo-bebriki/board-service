namespace Itmo.Bebriki.Boards.Application.Contracts.Boards.Exceptions;

public sealed class BoardNotFoundException : Exception
{
    public BoardNotFoundException() { }

    public BoardNotFoundException(string message) : base(message) { }

    public BoardNotFoundException(string message, Exception inner) : base(message, inner) { }
}