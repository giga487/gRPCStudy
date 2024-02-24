
using Core.Server;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            GameServer gameServer = new GameServer();
            builder.Services.AddSingleton(gameServer);

            // Add services to the container.
            builder.Services.AddGrpc();
            // ADD AUTHENTICATIO WITH JWT

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<Services.CharStatusService>();
            app.MapGrpcService<Services.AccountLoginServices>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}