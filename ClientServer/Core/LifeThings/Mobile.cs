using Core.LifeThings;
using Core.Space;

namespace Core.Mobile
{
    public abstract class Mobile : ILocalizable
    {
        public string Name { get; set; } = string.Empty;
        public Pose Pose { get; set; } = new Pose();

        //public virtual MoveAck Move(DeltaPosition pos)
        //{

        //    return new MoveAck()
        //    {
        //        Result = MoveResult.Ok
        //    };
        //}
    }
}
