﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBData
{
    public class DBInterface
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

        public Dictionary<string, string> AccountGUIDs = new Dictionary<string, string>();

        public DBInterface()
        {
            //PER ORA NON FA UN CAZZO
        }

        public virtual DB_RESPONSE_TYPE Add<T>()
        {
            return DB_RESPONSE_TYPE.OK;
        }
    }
}
