using Core.Mobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Player
{
    public class Player : Mobile.LifeThings.Mobile
    {
        Guid authenticationToken = Guid.Empty;
        public Player(ILocalizableProperties props) : base(props.Name, props)
        {

        }



        //public override MoveAck Move(DeltaPosition pos)
        //{
        //    if (Service is null)
        //    {
        //        return new MoveAck()
        //        {
        //            Result = MoveResult.BadCommunication
        //        };
        //    }

        //    return base.Move(pos);
        //}
    }
}
