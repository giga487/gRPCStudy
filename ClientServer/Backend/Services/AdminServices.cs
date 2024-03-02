using AdminCommand;
using Core.Server;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;

namespace Backend.Services
{
    public class AdminCommandServices: AdminCommand.AdminCommand.AdminCommandBase
    {
        private GameServer? _server { get; set; } = null;
        private SerialManager? _serialM { get; set; } = null;

        public AdminCommandServices(GameServer? server) 
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            _server = server;

            if (_server is not null && _server.SerialManager is SerialManager sm)
                _serialM = sm;
        }

        public override Task<AdminResponse> Save(AdminCmd request, ServerCallContext context)
        {
            _server.Save();

            return Task.FromResult(new AdminResponse() { ServerAnwser = AdminResponseT.Ok });

        }
    }
}
