using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.Util;
using Robocode.RobotInterfaces;

namespace Nervii
{
    public static class Helpers
    {
        public enum TurnType
        {
            Robot,
            Gun,
            Radar
        }
        public enum TurnDirection
        {
            Left,
            Right
        }

        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public static double GetAngle(double x1, double y1, double x2, double y2)
        {

            double xo = x2 - x1;
            double yo = y2 - y1;
            double hyp = GetDistance(x1, y1, x2, y2);
            double arcSin = Utils.ToDegrees(Math.Asin(xo / hyp));
            double bearing = 0;

            if (xo > 0 && yo > 0)
            {
                // both pos: lower-Left
                bearing = arcSin;
            }
            else if (xo < 0 && yo > 0)
            {
                // x neg, y pos: lower-right
                bearing = 360 + arcSin; // arcsin is negative here, actuall 360 - ang
            }
            else if (xo > 0 && yo < 0)
            {
                // x pos, y neg: upper-left
                bearing = 180 - arcSin;
            }
            else if (xo < 0 && yo < 0)
            {
                // both neg: upper-right
                bearing = 180 - arcSin; // arcsin is negative here, actually 180 + ang
            }
            return bearing;
        }


    }
}
