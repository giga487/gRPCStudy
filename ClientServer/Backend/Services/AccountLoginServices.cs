using Account;
using CharStatus;
using Grpc.Core;

namespace Backend.Services
{
    public class AccountLoginServices : Account.AccountLogin.AccountLoginBase
    {
        public override Task<AccountResponse> Login(Account.Account request, ServerCallContext context)
        {


            var rs = new AccountResponse()
            {
                Response = AccountAck.Ok,
                Hash = Guid.NewGuid().ToString()
            };

            Console.WriteLine($"Account {request.Email} has logged - {rs.Hash}");
            return Task.FromResult(rs);
        }
    }
}
