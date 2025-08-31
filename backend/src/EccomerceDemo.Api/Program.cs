
using EccomerceDemo.Infrastructure;
using EccomerceDemo.Presentation;
using EccomerceDemo.Application;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddPresentationLayer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UsePresentationLayer();

await app.RunAsync();

public partial class Program
{
    protected Program() { }
}
