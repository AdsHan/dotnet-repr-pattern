using MediatR;

namespace ReprPattern.API.Common;

public interface ICachedQuery<T> : IRequest<T>, ICachedQuery
{

}

public interface ICachedQuery
{
    string Key { get; }
    TimeSpan Expiration { get; }
}
