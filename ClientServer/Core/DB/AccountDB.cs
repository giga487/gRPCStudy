using Core.Account;
using DBData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Core.Mobile;

namespace Core.DB.Account
{
    public class AccountDB : DBInterface
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

        public enum PG_CREATION_RESULT
        {
            OK,
            BAD
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

        public PG_CREATION_RESULT CreateNewCharacter(AccountInfo info, ILocalizableProperties prop)
        {
            try
            {
                if (_dict.TryGetValue(info.UserName, out var value))
                {
                    value?.Characters?.Add(new Core.Player.Player(prop));
                    _logger?.LogInformation("Update account with new character");
                    return PG_CREATION_RESULT.OK;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning($"BAD {ex.Message}");
                return PG_CREATION_RESULT.BAD;
            }

            return PG_CREATION_RESULT.OK;
        }

        public AccountInfo? RetrieveAccountInfo(string user)
        {
            AccountInfo? accountInfo = null;

            try
            {
                if (_dict.TryGetValue(user, out accountInfo))
                {
                    return accountInfo;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogWarning($"BAD {ex.Message}");
            }

            return accountInfo ?? null;

        }

        /// <summary>
        /// If account username and hash is ok, you can login
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public bool HashIsOk(string hash, AccountInfo accountInfo)
        {
            if (string.Compare(hash, accountInfo?.Hash, true) == 0)
            {
                return true;
            }

            return false;

        }
    }
}
