using LogisticsSolution.Api.Extensions;
using LogisticsSolution.Application.Constant;
using LogisticsSolution.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    //cors
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        options.AddPolicy("SignalRPolicy", builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true);
        });
    });

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "User Authorization (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
            };
        });

    // Register Repository and Services
    builder.Services.AddHttpContextAccessor();
    builder.Services.InjectRepository();
    builder.Services.InjectService();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // ⚠️ Configure Kestrel to listen on platform-provided PORT or fallback to 8080
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(int.Parse(port));
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        // No dev-specific middleware added
    }

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseRouting();

    app.UseCors("SignalRPolicy");

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // ✅ Health check route for Koyeb or similar platforms
    app.MapGet("/", () => "API is healthy");

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<NotificationHub>("/notification");
    });

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
