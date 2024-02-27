using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Account
{
    public class Token
    {
        public TimeSpan Seconds { get; } = TimeSpan.FromHours(10);
        public string Hash { get; set; } = string.Empty;
        public DateTime CreationTime { get; set;  } = DateTime.Now;
        public DateTime ExpiringTime { get; set; } = DateTime.Now;
        public Token()
        {
            ExpiringTime = CreationTime + Seconds;
            Hash = Guid.NewGuid().ToString();
        }
    }
}
