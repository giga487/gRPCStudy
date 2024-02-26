using Core.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mobile.LifeThings
{

    public interface ILocalizable
    {
        string Name { get; }
        Pose Pose { get; }
    }
}
