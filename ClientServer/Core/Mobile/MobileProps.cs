using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mobile
{
    public interface ILocalizableProperties
    {
        string Name { get; }
        HairType HairType { get; }

    }
}
