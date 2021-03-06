using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using TestBarberPWA.Client;
using TestBarberPWA.Client.Services;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg4MDA4QDMxMzkyZTM0MmUzMEcyQ3BVMUdRRWJvNUwrRisxMjRaNjhFT0VSMEI0MVVRSWxKQW12b2IzM2s9;NTg4MDA5QDMxMzkyZTM0MmUzMFBtZllzUGRhSFpaSW5XYUVEbDBkb2RONlFSVGU3d3kva29HZHV5MFoxeUU9;NTg4MDEwQDMxMzkyZTM0MmUzMFQya0dNTmY1WjBXdDUyeWpXTFI3eHpPWU1VZGliNDYzVTUvSjBmRDF6WG89;NTg4MDExQDMxMzkyZTM0MmUzMEhlODdhdzI5SE96YzdncnIwOGlvTFNSS0VReU03S0VNc0NjcGd1QkZrT2c9;NTg4MDEyQDMxMzkyZTM0MmUzMEwrdElsK05MVmZIeXB2NU16bGZzUWYzTGdSRFp4aWRZUVZldlI4UkU1RGs9;NTg4MDEzQDMxMzkyZTM0MmUzMFFxa0RkYnV0NFZMcmdadjZKSGlZV3FXZ25SZXk2QXFLMzdMQnRBN3F5UVU9;NTg4MDE0QDMxMzkyZTM0MmUzMGpaNEo4V2lsMXdtdDhvd2hSTnNrS1FyR2lNeW1RT0lUSDRGUytoY2RiUnM9;NTg4MDE1QDMxMzkyZTM0MmUzMEhLVy80dFo0Q2lxL1VvYStEZVBjenRzVHZ0VVRCT0k0QlBocjJjQTJGZFE9;NTg4MDE2QDMxMzkyZTM0MmUzME51dXlCQnZFMS9rQzVwRGlrWDc4TGRWWjB2YUlLN0lMTSs2bDhHdnBXRG89;NTg4MDE3QDMxMzkyZTM0MmUzMFR2NTJwMXNYclkwVmxmV1paRnJBbVlYN1NTL0xjK0hYZ3RKbVY5TGhZWk09;NTg4MDE4QDMxMzkyZTM0MmUzMGo0OGxwZlNKamNQVzVNa1MrZTNhWklBOWY2eDc0NFhkWGxZdXMwZEJsZFk9");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IPeopleService, PeopleService>(client =>
{
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<PeopleAdapter>();

await builder.Build().RunAsync();
