using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paganod.Web.Tests;

/*
 * https://steinbach.io/asp-net-core-e2e-tests-with-xunit-and-playwright/
 * https://gitlab.com/steinbachiosamples/blazore2e/-/tree/v1
 */

public class WebApplicationFactoryFixture : WebApplicationFactory<Program>
{
    public WebApplicationFactoryFixture()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseUrls("https://localhost:7048");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // need to create a plain host that we can return.
        var dummyHost = builder.Build();

        // configure and start the actual host.
        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());

        var host = builder.Build();
        host.Start();

        return dummyHost;
    }
}
