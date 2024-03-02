using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Server
{
    public class LoadConfiguration
    {
        public SerialManageConfiguration? SerialConfig { get; private set; } = null;
        public DBAccountConfiguration? DBConfig { get; private set; } = null;
        public string Folder { get; private set; } = string.Empty;
        public LoadConfiguration(string folder)
        {
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            Folder = folder;

            SerialConfig = new SerialManageConfiguration("Serial.json", folder) ?? null;
            DBConfig = new DBAccountConfiguration("DBConfig.json", folder) ?? null;

        }
    }


    public class SerialManageConfiguration : Configuration
    {
        public SerialManageConfiguration(string file, string path) : base(file, path)
        {

        }
    }

    public class DBAccountConfiguration : Configuration
    {
        public DBAccountConfiguration(string file, string path) : base(file, path)
        {

        }
    }

    public abstract class Configuration
    {
        public string? FileName { get; private set; } = string.Empty;
        public string? ConfigPath { get; private set; } = string.Empty;
        public string? _file { get; set; } = string.Empty;
        public Configuration(string? filename, string path)
        {
            if (filename is null)
                throw new Exception("Configuration file name is missing");

            FileName = filename;
            ConfigPath = path;

            _file = Path.Combine(ConfigPath, FileName);
            if (!File.Exists(_file))
            {
                Save();
            }

            Load();

        }

        public Configuration? Load()
        {
            if (_file is null)
                throw new FileNotFoundException();

            try
            {
                JsonConvert.DeserializeObject<Configuration>(_file);
            }
            catch
            {
                return null;
            }

            return this;
        }

        public Configuration Save()
        {

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            if (FileName is null)
                throw new FileNotFoundException();

            using (FileStream fs = File.Open(FileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    using (JsonTextWriter jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;
                        jw.Indentation = 4;
                        //jw.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;

                        var jsonSerializer = new JsonSerializer();
                        string json = JsonConvert.SerializeObject(this, settings);
                        jsonSerializer.Serialize(jw, json);
                    }
                }
            }

            return this;
        }
    }
}
