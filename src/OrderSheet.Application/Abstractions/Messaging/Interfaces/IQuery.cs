using MediatR;

namespace OrderSheet.Application.Abstractions.Messaging.Interfaces
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
