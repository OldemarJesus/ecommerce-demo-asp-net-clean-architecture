using EccomerceDemo.Domain.Abstractions;

namespace EccomerceDemo.Application.Abstrations.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
