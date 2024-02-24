using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Movable
{
    public class Player : Movable.IMobile
    {
        public string Name { get; set; } = string.Empty;
    }
}
