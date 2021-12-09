using MediatR;

namespace OrderSheet.Application.Abstractions.Messaging.Interfaces
{
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}
