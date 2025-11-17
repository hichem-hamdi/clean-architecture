using Application.Abstractions.Messaging;
using Application.Products.Create;
using Application.Users.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	public sealed record CreateRequest(string Label, decimal? Price);

	[HttpPost]
	public async Task<IResult> Create(CreateRequest request,
		ICommandHandler<CreateProductCommand, Guid> handler,
		CancellationToken cancellationToken)
	{
		var command = new CreateProductCommand
		{
			Label = request.Label,
			Price = request.Price.GetValueOrDefault()
		};

		Result<Guid> result = await handler.Handle(command, cancellationToken);
        
        return result.Match(Results.Ok, CustomResults.Problem);
	}
}
