using MediatR;

namespace OrderSheet.Application.Abstractions.Messaging.Interfaces
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
