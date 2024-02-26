using AccountProtocol;
using CharStatus;
using Core.Account;
using Core.Mobile;
using Core.Player;
using Core.Server;
using Grpc.Core;

namespace Backend.Services
{
    public class AccountLoginServices : AccountLogin.AccountLoginBase
    {
        public GameServer? GameServer { get; set; }
        public AccountLoginServices(GameServer server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            GameServer = server;
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
                };

                if (GameServer?.AccountInterface?.AddAccount(newAccount) != Core.DB.Account.AccountDB.LOGIN_RESULT.OK)
                {
                    rs.Response = AccountAck.GenericError;
                    return Task.FromResult(rs);
                }

                rs.Response = AccountAck.Ok;
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

                foreach (var character in requestAccount.Characters)
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

            var infoAccount = GameServer?.AccountInterface?.RetrieveAccountInfo(request.Name);

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

            if (infoAccount is not null && GameServer?.AccountInterface?.CreateNewCharacter(infoAccount, charProperties) == Core.DB.Account.AccountDB.PG_CREATION_RESULT.OK)
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
