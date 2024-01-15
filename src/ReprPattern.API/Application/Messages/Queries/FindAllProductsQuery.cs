using MediatR;
using ReprPattern.API.Data.Entities;

namespace ReprPattern.API.Application.Messages.Queries;

public record FindAllProductsQuery : IRequest<List<ProductModel>>;