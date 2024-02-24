using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer.Login
{
    public class ClientLogin
    {
        public GrpcChannel? Channel { get; private set; } = null;
        public Account.AccountLogin.AccountLoginClient AccountLoginClient { get; private set; }
        public ClientLogin(Uri host)
        {
            Channel = GrpcChannel.ForAddress(host);

            AccountLoginClient = new Account.AccountLogin.AccountLoginClient(Channel);

            //Action loginAction = () =>
            //{
            //    if (Channel != null)
            //    {
            //        var reply = Login(user, pwd);
            //    }
            //};

            //Task.Run(loginAction);

        }

        public async Task<Account.AccountResponse> Login(string user, string pwd)
        {
            try
            {
                var reply = await AccountLoginClient.LoginAsync(new Account.Account() { Email = user, Password = pwd });

                if (reply != null)
                {
                    return reply;
                }

                throw new Exception("No reply from server");
            }
            catch
            {
                return new Account.AccountResponse()
                {
                    Response = Account.AccountAck.BadConnection
                };
            }
        }

    }
}
