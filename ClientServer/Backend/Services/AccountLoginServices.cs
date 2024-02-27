using AccountProtocol;
using CharStatus;
using Core.Account;
using Core.Mobile;
using Core.Player;
using Core.Server;
using Grpc.Core;
using System.Security.Cryptography;

namespace Backend.Services
{
    public class AccountLoginServices : AccountLogin.AccountLoginBase
    {
        public GameServer? GameServer { get; set; } = null;
        public SerialManager? SerialManager { get; set; } = null;
        public AccountLoginServices(GameServer server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            GameServer = server;

            if (GameServer is not null)
                SerialManager = GameServer?.SerialManager;
        }

        /// <summary>
        /// OVVIAMENTE QUESTA PARTE VA RISCRITTA CON SCIENZA, vorrei inserire che se un account con quell'username non c'è, viene inventato
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<AccountResponse> Login(Account request, ServerCallContext context)
        {
            var requestAccount = new AccountInfo()
            {
                UserName = request.Email,
                Password = request.Password,
            };

            var status = GameServer?.Auth?.AuthenticationStatus(request.Email, request.Password, out requestAccount);

            var rs = new AccountResponse()
            {
                Response = AccountAck.GenericError
            };

            if (status is Authentication.AuthenticationT.ACCOUNT_NOT_EXIST)
            {
                var newAccount = new AccountInfo()
                {
                    UserName = request.Email,
                    Password = request.Password,
                    Token = new Token()
                };

                if (GameServer?.AccountInterface?.AddAccount(newAccount) != Core.DB.Account.AccountDB.LOGIN_RESULT.OK)
                {
                    rs.Response = AccountAck.GenericError;
                    //rs.Hash = newAccount.Token.Hash;

                    //if the account is new, there is no PG

                    return Task.FromResult(rs);
                }

                rs.Response = AccountAck.Ok;
                rs.Hash = newAccount.Token.Hash;
                return Task.FromResult(rs);
                //CREATE
            }
            else if (status is Authentication.AuthenticationT.BADPASSWORD)
            {
                rs.Response = AccountAck.WrongPassword;
                return Task.FromResult(rs);
            }
            else if(status is Authentication.AuthenticationT.OK && requestAccount is not null)
            {
                rs.Response = AccountAck.Ok;
                rs.Hash = requestAccount?.Token?.Hash;

                foreach (var character in requestAccount?.Characters)
                {
                    if (character.Serial is not null)
                    {
                        rs.CharSerial.Add(character.Serial.ID);
                    }
                }

                return Task.FromResult(rs);
            }

            return Task.FromResult(rs);
        }



        public override Task<NewCharResponse>? CreateNewChar(NewChar request, ServerCallContext context)
        {
            var charProperties = new PlayerProperties()
            {
                HairType = (HairType)request.Hair,
                Name = request.Name,
            };

            var infoAccount = GameServer?.AccountInterface?.RetrieveAccountInfo(request.Username);

            if (infoAccount is null)
            {
                return Task.FromResult(new NewCharResponse()
                {
                    Answer = NewCharResponseT.NoPg
                });
            }

            if(!GameServer.AccountInterface.HashIsOk(request.Token, infoAccount))
            {
                return Task.FromResult(new NewCharResponse()
                {
                    Answer = NewCharResponseT.NoPg
                });
            }

            if(infoAccount is null)
            {
                return Task.FromResult(new NewCharResponse()
                {
                    Answer = NewCharResponseT.NoPg
                });
            }

            if(GameServer?.AccountInterface?.CreateNewCharacter(infoAccount, charProperties) == Core.DB.Account.AccountDB.PG_CREATION_RESULT.OK)
            {
                return Task.FromResult(new NewCharResponse()
                {
                    Answer = NewCharResponseT.PgCreated
                });
            }
            else
            {
                return Task.FromResult(new NewCharResponse()
                {
                    Answer = NewCharResponseT.NoPg
                });
            }
        }
    }
}
