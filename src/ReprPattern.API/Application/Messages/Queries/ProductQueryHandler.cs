using MediatR;
using ReprPattern.API.Data.Entities;
using ReprPattern.API.Data.Repositories;

namespace ReprPattern.API.Application.Messages.Queries;

public class ProductQueryHandler : IRequestHandler<FindAllProductsQuery, List<ProductModel>>

{
    private readonly IProductRepository _productRepository;

    public ProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductModel>> Handle(FindAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();

        return products.ToList();
    }
}
