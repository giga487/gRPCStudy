using Core.Server;
using Core.Space;

namespace Core.Mobile.LifeThings
{
    public abstract class Mobile : ILocalizable, ISerializable
    {
        public string Name { get; set; } = string.Empty;
        public Pose Pose { get; set; } = new Pose();
        public ILocalizableProperties? AestheticProperties { get; private set; } = null;
        public Serial? Serial { get; set; } = null;

        public Mobile(string name, ILocalizableProperties props)
        {
            Name = name;
            AestheticProperties = props;


        }

        public void Deserialize()
        {
            throw new NotImplementedException();
        }

        public void Serialize()
        {
            throw new NotImplementedException();
        }
        //public abstract ILocalizable CreateMobile(ILocalizableProperties props, string name);

        //public virtual MoveAck Move(DeltaPosition pos)
        //{

        //    return new MoveAck()
        //    {
        //        Result = MoveResult.Ok
        //    };
        //}
    }
}
