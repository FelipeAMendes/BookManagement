using BookManagement.Modules.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddMvcOptions(_ =>
    {
        //options.ModelValidatorProviders.Add(new CustomModelValidatorProvider());
    });
builder.Services.AddServices(builder.Configuration);
builder.Services.AddPageServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

app.ExecuteMigrations();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();