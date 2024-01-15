using FastEndpoints;
using FluentValidation;
using MediatR;
using ReprPattern.API.Application.Messages.Commands;

namespace ReprPattern.API.Endpoints.Products;

public record CreateProductsRequest(string Title, string Description, double Price, int Quantity);

public record CreateProductsReturn(int Id);

public class CreateProductValidator : Validator<CreateProductsRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("O título não foi informado!");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("O descrição não foi informado!");

        RuleFor(x => x.Price)
            .NotEmpty()
                .WithMessage("O preço não foi informado!");

        RuleFor(x => x.Quantity)
            .NotEmpty()
                .WithMessage("A quantidade não foi informado!");
    }
}

public class CreateProductsEndpoint : Endpoint<CreateProductsRequest, CreateProductsReturn>
{
    private readonly IMediator _mediator;

    public CreateProductsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/products");
        AllowAnonymous();
        Validator<CreateProductValidator>();
        Summary(s =>
        {
            s.Summary = "Grava o produto";
            s.ExampleRequest = new CreateProductsRequest("Sandalia", "Sandália Preta Couro Salto Fino", 249.50, 100);
            s.Response(201, "O produto foi incluído corretamente");
            s.Response(400, "Falha na requisição");
        });
    }

    public override async Task HandleAsync(CreateProductsRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new CreateProductCommand(request.Title, request.Description, request.Price, request.Quantity));

        if (!result.IsValid())
        {
            AddError(string.Concat(result.Errors.ToArray()), "400");

            ThrowIfAnyErrors();
        }

        await SendCreatedAtAsync("NewProduct", request, new CreateProductsReturn(int.Parse(result.Response.ToString())));
    }
}





