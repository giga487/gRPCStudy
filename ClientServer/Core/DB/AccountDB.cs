using Core.Account;
using DBData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Core.DB.Account
{
    public class AccountDB: DBInterface
    {
        public enum LOGIN_RESULT
        {
            DEFAULT,
            OK,
            BADPASSWORD,
            BANNED,
            BLOCKED,
            LAMER,
            YOURMUMFAULT
        }

        private Dictionary<string, AccountInfo> _dict = new Dictionary<string, AccountInfo>();
        private ILogger? _logger { get; set; } = null;
        public AccountDB(ILogger? logger)
        {
            if (logger != null)
                _logger= logger;
        }

        public LOGIN_RESULT UpdateLoginInfo(AccountInfo account)
        {
            if(!_dict.ContainsKey(account.UserName) && AddAccount(account) != LOGIN_RESULT.OK)
            {
                return LOGIN_RESULT.DEFAULT;
            }
            else if(UpdateAccount(account) != LOGIN_RESULT.OK)
            {
                return LOGIN_RESULT.DEFAULT;
            }

            return LOGIN_RESULT.OK;
        }

        public LOGIN_RESULT AddAccount(AccountInfo info)
        {
            try
            {
                if (_dict.TryAdd(info.UserName, info))
                {
                    _logger?.LogInformation("Inserted in account DB");
                }
            }
            catch(Exception ex)
            {
                _logger?.LogWarning($"BAD {ex.Message}");
                return LOGIN_RESULT.BANNED;
            }

            return LOGIN_RESULT.OK;
        }

        public LOGIN_RESULT UpdateAccount(AccountInfo info)
        {
            try
            {
                _dict[info.UserName] = info;
                _logger?.LogInformation("Update account DB with new LOGIN Info");
            }
            catch (Exception ex)
            {
                _logger?.LogWarning($"BAD {ex.Message}");
                return LOGIN_RESULT.BANNED;
            }

            return LOGIN_RESULT.OK;
        }

    }
}
