using LogisticsSolution.Application.Constant;
using Microsoft.OpenApi.Models;
using LogisticsSolution.Application.Contract.Notification;
using LogisticsSolution.Application.Utility;
using LogisticsSolution.Infrastructure.Notification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using LogisticsSolution.Application.Models; // Add this to the top

var builder = WebApplication.CreateBuilder(args);

// Bind Kestrel to 0.0.0.0:80 (required for Docker + Koyeb)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy", policy =>
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowAnyOrigin());
});

// MongoDB Config
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbService<EmailLog>>();

// AppSettings binding
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Register Notification service and SignalR
builder.Services.AddScoped<INotification, NotificationService>();
builder.Services.AddSignalR();

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseCors("SignalRPolicy");
app.UseAuthentication();
app.UseAuthorization();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Map endpoints
app.MapControllers();
app.MapGet("/health", () => Results.Ok("Healthy"));
app.MapGet("/", () => Results.Ok("Zinter API is live 🚀")); // 👈 Add this
app.MapHub<NotificationHub>("/notification");

app.Run();

