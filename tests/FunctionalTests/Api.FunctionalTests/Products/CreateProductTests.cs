using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Api.FunctionalTests.Abstractions;
using Api.FunctionalTests.Contracts;
using FluentAssertions;
using Org.BouncyCastle.Crypto;
using Xunit;
using Api.FunctionalTests.Extensions;
using static Web.Api.Controllers.ProductsController;

namespace Api.FunctionalTests.Products;
public class CreateProductTests : BaseFunctionalTest
{
    public CreateProductTests(FunctionalTestWebApiFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenPriceIsMissing()
    {
        // Arrange
        var command = new CreateRequest("Test Product", null);

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/products", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Errors.Should().NotBeEmpty();
    }
}
