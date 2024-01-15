using FastEndpoints;
using MediatR;
using ReprPattern.API.Application.Messages.Queries;
using ReprPattern.API.Data.Entities;
using ReprPattern.API.Data.Enums;

namespace ReprPattern.API.Endpoints.Products;

public record GetProductsRequest();

public record GetProductsReturn()
{
    public string Description { get; set; }
    public EntityStatusEnum Status { get; set; }
}

public class ProductMapper : Mapper<GetProductsRequest, GetProductsReturn, ProductModel>
{
    public override GetProductsReturn FromEntity(ProductModel p) => new()
    {
        Description = p.Description,
        Status = p.Status,
    };
}

public class GetProductsEndpoint : Endpoint<GetProductsRequest, List<GetProductsReturn>, ProductMapper>
{
    private readonly IMediator _mediator;

    public GetProductsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/products");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Obtém todas os produtos realizando cache com Decorator Pattern";
            s.Response(200, "Sucesso");
            s.Response(204, "Nenhum registro localizado");
        });
    }

    public override async Task HandleAsync(GetProductsRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new FindAllProductsQuery());

        var response = result.Select(x => Map.FromEntity(x)).ToList();

        if (response is null)
        {
            await SendNoContentAsync();
        }
        else
        {
            await SendOkAsync(response);
        }
    }
}



