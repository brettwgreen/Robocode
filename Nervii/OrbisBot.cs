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
    public class OrbisBot : AdvancedRobot
    {

        private IRobotBehavior behavior;

        public OrbisBot()
        {
            behavior = new OrbisBehavior(this);
        }
      
        public override void Run()
        {
            behavior.Setup();
            while (true)
            {
                behavior.RunBehavior();
            }
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            behavior.OnHitByBulletBehavior(evnt);
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            behavior.OnHitRobotBehavior(evnt);
        }

        public override void OnScannedRobot(ScannedRobotEvent enemy)
        {
            behavior.OnScannedRobotBehavior(enemy);
        }

        public override void OnWin(WinEvent evnt)
        {
            behavior.OnWinBehavior(evnt);
        }

    }
}
