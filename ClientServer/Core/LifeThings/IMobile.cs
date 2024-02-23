using CharStatus;
using Core.Space;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LifeThings
{

    public interface ILocalizable
    {
        string Name { get; }
        Pose Pose { get; }        
        MoveAck Move(CharStatus.DeltaPosition pos);
    }
}
