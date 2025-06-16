var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("SignalRPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
    );
});

// MongoDB Config
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbService>();

var app = builder.Build();

// Middleware
app.UseRouting();
app.UseCors("SignalRPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok("Healthy"));

// SignalR
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notification");
});

app.Run();
