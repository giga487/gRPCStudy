using Core.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Space
{
    public class Position
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Z { get; set; } = 0;
    }

    public class Angle
    {
        public double R { get; set; } = 0;
        public double P { get; set; } = 0;
        public double Y { get; set; } = 0;
    }

    public class Pose
    {
        public Position Position { get; set; } = new Position();
        public Angle Angle { get; set; } = new Angle();
    }
}
