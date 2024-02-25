using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server
{
    public interface ISerializable
    {
        Serial Serial { get; }
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
