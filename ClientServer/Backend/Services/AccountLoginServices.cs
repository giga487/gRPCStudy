using Account;
using CharStatus;
using Core.Server;

//using Core.Server;
using Grpc.Core;

namespace Backend.Services
{
    public class AccountLoginServices : Account.AccountLogin.AccountLoginBase
    {
        public GameServer GameServer { get; set; }
        public AccountLoginServices(GameServer server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            GameServer = server;
        }


        public override Task<AccountResponse> Login(Account.Account request, ServerCallContext context)
        {
            var rs = new AccountResponse()
            {
                Response = AccountAck.Ok,
                Hash = Guid.NewGuid().ToString()
            };

            GameServer?.AccountInterface?.UpdateLoginInfo(new Core.Account.AccountInfo()
            {
                Hash = rs.Hash,
                Password = request.Password,
                UserName = request.Email
            });

            //Console.WriteLine($"Account {request.Email} has logged - {rs.Hash}");
            return Task.FromResult(rs);
        }
    }
}
