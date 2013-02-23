using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace Nervii
{
    public abstract class SingleAdvancedRobotBehavior : AdvancedRobot
    {

        abstract public IRobotBehavior RobotBehavior { get; }

        public SingleAdvancedRobotBehavior()
        {
        }
      
        public override void Run()
        {
            RobotBehavior.Setup();
            while (true)
            {
                RobotBehavior.RunBehavior();
            }
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            RobotBehavior.OnHitByBulletBehavior(evnt);
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            RobotBehavior.OnHitRobotBehavior(evnt);
        }

        public override void OnScannedRobot(ScannedRobotEvent enemy)
        {
            RobotBehavior.OnScannedRobotBehavior(enemy);
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            RobotBehavior.OnHitWallBehavior(evnt);
        }

        public override void OnWin(WinEvent evnt)
        {
            RobotBehavior.OnWinBehavior(evnt);
        }

    }
}
