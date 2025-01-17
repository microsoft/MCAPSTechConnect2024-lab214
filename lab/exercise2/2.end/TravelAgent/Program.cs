using Microsoft.Agents.Protocols.Primitives;
using Microsoft.SemanticKernel;
using Microsoft.Agents.Hosting.Setup;
using SingleAgent.Bots;
using SingleAgent.Agents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddKernel();

builder.Services.AddAzureOpenAIChatCompletion(
       deploymentName: builder.Configuration.GetSection("AIServices:AzureOpenAI").GetValue<string>("DeploymentName"),
       endpoint: builder.Configuration.GetSection("AIServices:AzureOpenAI").GetValue<string>("Endpoint"),
       apiKey: builder.Configuration.GetSection("AIServices:AzureOpenAI").GetValue<string>("ApiKey"));

builder.Services.AddTransient<TravelAgent>();
builder.AddBot<IBot, BasicBot>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", () => "Microsoft 365 Agents SDK Sample");
    app.UseDeveloperExceptionPage();
    app.MapControllers().AllowAnonymous();
}

app.MapControllers();

app.Run();
