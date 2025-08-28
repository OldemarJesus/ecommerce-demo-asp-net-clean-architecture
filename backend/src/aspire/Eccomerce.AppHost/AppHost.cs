using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<EccomerceDemo_Api>("api");

await builder.Build().RunAsync();
