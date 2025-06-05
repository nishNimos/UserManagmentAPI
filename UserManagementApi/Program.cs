using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Middleware;

namespace UserApi;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
var secretKey = "a-string-secret-at-least-256-bits-long";
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<UserContext>(options =>
        options.UseInMemoryDatabase("UsersDb"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        // app.UseAuthorization();
        app.UseExceptionHandling();
        app.UseJwtAuthentication(secretKey);
        app.UseMiddleware<RequestLoggingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}