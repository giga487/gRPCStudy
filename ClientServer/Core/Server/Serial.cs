using Core.Account;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Server
{
    public class SerialManager(ILogger? logger, SerialManageConfiguration? config)
    {
        private ConcurrentDictionary<ulong, ISerializable> World { get; } = new ConcurrentDictionary<ulong, ISerializable>();
        public ulong LastID { get; private set; } = ulong.MaxValue;
        public SerialManageConfiguration? Configuration { get; private set; } = config;
        public void AddSerializable(ISerializable serializable)
        {
            if (!World.TryAdd(serializable.Serial.ID, serializable))
            {
                logger?.LogCritical($"{serializable.Serial.ID} is trying to add at world list");
                LastID = serializable.Serial.ID; //ONLY HERE
            }
            else
                World[serializable.Serial.ID] = serializable;
        }

        public ISerializable? GetSerializable(ulong serialID)
        {
            if (World.TryGetValue(serialID, out ISerializable? serializable))
            {
                return serializable;
            }
            else
            {
                return null;
            }
        }

        public void CreateNewSerial(ISerializable serializable)
        {
            serializable.Serial = new Serial(++LastID);

            AddSerializable(serializable);
        }

        public bool CreateNewCharacter(ISerializable? serializable, AccountInfo info)
        {
            if (serializable is null)
                return false;

            if (CharacterCanBeSerialized(info, serializable) && serializable is Player.Player pl)
            {
                info.Characters.Add(pl);
                CreateNewSerial(serializable);

                if(pl.Serial is Serial sm)
                {
                    info.CharactersSerial.Add(sm.ID);
                }

            }

            return true;
        }

        /// <summary>
        /// Here we can set limits and other stuffs
        /// </summary>
        /// <param name="info"></param>
        /// <param name="serializable"></param>
        /// <returns></returns>
        public bool CharacterCanBeSerialized(AccountInfo info, ISerializable serializable)
        {
            if (!CanBeSerialized(serializable))
                return false;

            return true;
        }
        public bool CanBeSerialized(ISerializable serializable)
        {
            return true;
        }

        public bool Save()
        {

            using (TextWriter textWriter = File.CreateText("World.json"))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer();

                foreach (var objToSave in World)
                {
                    serializer.Serialize(textWriter, objToSave);
                }
            }

            return true;
        }


    }

    public interface ISerializable
    {
        Serial Serial { get; internal set; }
        void Deserialize();
        void Serialize();
    }
    public class Serial
    {
        public ulong ID { get; private set; } = ulong.MaxValue;
        public DateTime Created { get; private set; } = DateTime.Now;
        public Serial(ulong id)
        {
            ID = id;
        }
    }
}
