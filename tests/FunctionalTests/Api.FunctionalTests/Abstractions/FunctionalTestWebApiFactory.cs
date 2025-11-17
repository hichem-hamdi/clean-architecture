using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Web.Api;
using Xunit;

namespace Api.FunctionalTests.Abstractions;

public class FunctionalTestWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    override protected void ConfigureWebHost(IWebHostBuilder builder)
    {
       builder.ConfigureTestServices(services =>
       {
           services.RemoveAll<DbContextOptions<ApplicationDbContext>>();

           services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_dbContainer.GetConnectionString()));
       });
    }

    public Task InitializeAsync()
    {
       return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
