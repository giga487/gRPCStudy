using Core.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBData
{
    public abstract class DBInterface
    {
        public class ConnectionError : Exception
        {
            public ConnectionError() : base("Database connection error")
            {

            }
        }

        public enum DB_RESPONSE_TYPE
        {
            OK,
            CONNECTION_BAD,
        }

        public DBInterface()
        {
            //PER ORA NON FA UN CAZZO
        }

        public virtual DB_RESPONSE_TYPE Update<T>(T account) where T: IDataInfo
        {
            return DB_RESPONSE_TYPE.OK;
        }
    }
}
