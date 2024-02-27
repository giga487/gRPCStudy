using Core.DB.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Account
{
    public class Authentication
    {

        public enum AuthenticationT
        {
            OK,
            BADPASSWORD,
            ACCOUNT_NOT_EXIST
        }

        public AccountDB? AuthenticationDB { get; set; } = null;
        //UN SACCO DI COSE
        public Authentication(AccountDB dbAccount)
        {
            AuthenticationDB = dbAccount;
        }

        /// <summary>
        /// MOCK AUTHENTICATION STATUS
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticationT? AuthenticationStatus(string username, string password, out AccountInfo? info)
        {
            var accountInfo = AuthenticationDB?.RetrieveAccountInfo(username);

            info = null;

            if (accountInfo is null)
                return AuthenticationT.ACCOUNT_NOT_EXIST;

            if (AuthIsOk(username, password, accountInfo))
            {
                accountInfo.Token = new Token();
                info = accountInfo;

                return AuthenticationT.OK;
            }
            else
                return AuthenticationT.BADPASSWORD;
        }



        public bool AuthIsOk(string username, string password, AccountInfo? info)
        {
            if (info is null)
                return false;

            return true;
        }

    }
}
