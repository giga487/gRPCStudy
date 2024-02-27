using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server
{
    public class LoadConfiguration()
    {
        public SerialManageConfiguration? SerialConfig { get; private set; } = null;
        public DBAccountConfiguration? DBConfig { get; private set; } = null;
    }


    public class SerialManageConfiguration : Configuration
    {
        public SerialManageConfiguration(string loadingFile) : base(loadingFile)
        {
        }
    }

    public class DBAccountConfiguration : Configuration
    {
        public DBAccountConfiguration(string loadingFile) : base(loadingFile)
        {
        }
    }

    public abstract class Configuration
    {
        private string _loadingFilePath { get; set; } = string.Empty;
        public Configuration(string loadingFile)
        {
            _loadingFilePath = loadingFile;
        }

        public Configuration Load()
        {
            return this;
        }
    }
}
