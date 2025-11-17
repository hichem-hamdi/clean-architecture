using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Api.FunctionalTests.Contracts;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Api.FunctionalTests.Extensions;
internal static class HttpResponseMessageExtensions
{
    internal static async Task<CustomProblemDetails> GetProblemDetails(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Cannot get ProblemDetails from a successful response.");
        }

        CustomProblemDetails problemDetails = await response.Content.ReadFromJsonAsync<CustomProblemDetails>();

        return problemDetails;
    }
}
