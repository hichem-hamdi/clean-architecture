using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.FunctionalTests.Abstractions;
public class BaseFunctionalTest : IClassFixture<FunctionalTestWebApiFactory>
{
    protected HttpClient HttpClient { get; init; }

    public BaseFunctionalTest(FunctionalTestWebApiFactory factory)
    {
        HttpClient = factory.CreateClient();
    }
}
