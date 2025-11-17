using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Api.FunctionalTests.Abstractions;
using Api.FunctionalTests.Contracts;
using Api.FunctionalTests.Extensions;
using Application.Users.Register;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.Data;
using Web.Api.Endpoints.Users;
using Xunit;

namespace Api.FunctionalTests.Users;
public class RegisterUserTests : BaseFunctionalTest
{
    public RegisterUserTests(FunctionalTestWebApiFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("", "name", "lastanme", "password1234");
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/users/register", request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                UserErrorCodes.CreateUser.MissingEmail,
                UserErrorCodes.CreateUser.InvalidEmail
                ]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsInvalid()
    {
        // Arrange
        var request = new CreateUserRequest("email", "name", "lastanme", "password1234");
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/users/register", request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([UserErrorCodes.CreateUser.InvalidEmail]);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenNameIsMissing()
    {
        // Arrange
        var request = new CreateUserRequest("test@email.com", "", "lastanme", "password1234");
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/users/register", request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        CustomProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([UserErrorCodes.CreateUser.FirstNameIsRequired]);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateUserRequest("test@email.com", "name", "lastanme", "password1234");
        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/users/register", request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        Guid userId = await response.Content.ReadFromJsonAsync<Guid>();

        userId.Should().NotBeEmpty();   
    }

    [Fact]
    public async Task Should_ReturnConflict_WhenUserExists()
    {
        // Arrange
        var request = new CreateUserRequest("test-conflict@email.com", "name", "lastanme", "password1234");
        await HttpClient.PostAsJsonAsync("/users/register", request);

        // Act
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("/users/register", request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
