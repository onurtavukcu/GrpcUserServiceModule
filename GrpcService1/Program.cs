using GrpcService1.AppDbContext;
using GrpcService1.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GrpcService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();

            builder.Services.AddDbContext<ApplicationDbContext>
                (
                      options => options
                      .UseNpgsql
                      (
                          builder.Configuration.GetConnectionString("GRPCTest"),
                          b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
                      )
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGrpcService<UserService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            app.MapGet("/test", () => "Test");
            app.Run();
        }
    }
}