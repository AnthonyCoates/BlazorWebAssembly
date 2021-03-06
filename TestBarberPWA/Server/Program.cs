using Microsoft.AspNetCore.ResponseCompression;
using TestBarberPWA.Client.Services;
using TestBarberPWA.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDBContext>
    (
        options => options.EnableSensitiveDataLogging() //Just need this lambda to debug the skip and take values for server-side paging
    );

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
builder.Services.AddScoped<IServicesSoldRepository, ServicesSoldRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
