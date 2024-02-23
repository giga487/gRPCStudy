using CharStatus;
using CommunicationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LifeThings
{
    public class Player: Mobile.Mobile
    {
        CharStatusService Service { get; set; } = null;
        public override MoveAck Move(DeltaPosition pos)
        {
            if (Service is null)
            {
                return new MoveAck()
                {
                    Result = MoveResult.BadCommunication
                };
            }

            return base.Move(pos);
        }
    }
}
