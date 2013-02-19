using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.RobotInterfaces;

namespace Nervii
{
    public interface IRobotBehavior
    {
        AdvancedRobot Robot { get; set; }
        string BehaviorName { get; }
        void Setup();
        void RunBehavior();
        void OnScannedRobotBehavior(ScannedRobotEvent enemy);
        void OnHitByBulletBehavior(HitByBulletEvent evnt);
        void OnHitRobotBehavior(HitRobotEvent evnt);
        void OnHitWallBehavior(HitWallEvent evnt);
        void OnWinBehavior(WinEvent evnt);

    }
}
