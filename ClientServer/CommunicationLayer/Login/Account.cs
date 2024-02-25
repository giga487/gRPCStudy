using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommunicationLayer.Login
{
    public class LoggedAccount
    {
        public string User { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public List<ulong> CharsID { get; set; } = new List<ulong>();

    }
}
