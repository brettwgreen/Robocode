using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Robocode;
using Robocode.Util;

namespace Nervii
{
    public class ShockWave : SingleRobotBehavior
    {

        private IRobotBehavior robotBehavior;

        public override IRobotBehavior RobotBehavior
        {
            get
            {
                if (robotBehavior == null)
                {
                    robotBehavior = new ShockWaveBehavior(this);
                }
                return robotBehavior;
            }
        }

    }
}
