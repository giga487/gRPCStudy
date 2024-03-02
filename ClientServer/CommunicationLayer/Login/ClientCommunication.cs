﻿using AccountProtocol;
using AdminCommand;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer.Login
{
    public class ClientCommunication
    {
        public GrpcChannel? Channel { get; private set; } = null;
        public AccountProtocol.AccountLogin.AccountLoginClient AccountLoginClient { get; private set; }
        public AdminCommand.AdminCommand.AdminCommandClient AdminCommandClient { get; private set; }
        public ClientCommunicationLayer.Login.LoggedAccount? Account { get; private set; } = null;
        public string Token { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public ClientCommunication(Uri host)
        {
            Channel = GrpcChannel.ForAddress(host);

            AccountLoginClient = new AccountProtocol.AccountLogin.AccountLoginClient(Channel);
            AdminCommandClient = new AdminCommand.AdminCommand.AdminCommandClient(Channel);
        }

        public async Task<AccountProtocol.AccountResponse> Login(string user, string pwd)
        {
            try
            {
                var reply = await AccountLoginClient.LoginAsync(new AccountProtocol.Account() { Email = user, Password = pwd });

                if (reply != null)
                {
                    UserName = user;
                    Token = reply.Hash;
                    return reply;
                }

                throw new Exception("No reply from server");
            }
            catch
            {
                return new AccountProtocol.AccountResponse()
                {
                    Response = AccountAck.BadConnection
                };
            }
        }

        public async Task<NewCharResponseT> CreateNewCharacter(string name)
        {
            var newChar = new NewChar()
            {
                Hair = HairStyleT.Bald,
                Token = Token,
                Name = name,
                Username = UserName
            };

            try
            {
                var reply = await AccountLoginClient.CreateNewCharAsync(newChar);
                return reply.Answer;
            }
            catch
            {
                return NewCharResponseT.NoPg;
            }     
        }

        public async Task<AdminCommand.AdminResponse>? AdminSave()
        {
            try
            {
                var reply = await AdminCommandClient.SaveAsync(new AdminCommand.AdminCmd());
                return reply;
            }
            catch
            {
                return null;
            }
        }


    }
}
