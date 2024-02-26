using Core.Mobile;
using Core.Mobile.LifeThings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Player
{
    public class PlayerProperties : ILocalizableProperties
    {
        public string Name { get; set; } = string.Empty;
        public HairType HairType { get; set; } = HairType.Bald;
    }
}
