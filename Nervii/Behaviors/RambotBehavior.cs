using System.Drawing;
using Robocode;
using System;
using Nervii;

namespace Nervii
{
    public class RambotBehavior : IRobotBehavior
    {
        public AdvancedRobot Robot { get; set; }

        public string BehaviorName { get { return "Rambot"; } }

        public RambotBehavior(AdvancedRobot robot)
        {
            Robot = robot;
        }

        public void Setup()
        {
            Robot.IsAdjustGunForRobotTurn = true;
            Robot.SetColors(Color.Yellow, Color.Fuchsia, Color.ForestGreen);
        }

        public void RunBehavior()
        {
            Robot.MoveToPoint(400, 400);
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            Robot.ClearAllEvents();
            TurnUntilYouLock(enemy);
            Robot.Ahead(enemy.Distance/4); 
            Robot.Scan();
        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            var heading = evnt.Heading;
            var roboHeading = Robot.Heading;

            Robot.TurnLeft(90);
            Robot.Ahead(500);

        }

        public void OnHitRobotBehavior(HitRobotEvent evnt)
        {
            Robot.TurnLeft(50);
            SpinUntilYouSeeSomething();
        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {
            Robot.TurnLeft(180);
            Robot.Ahead(500);
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            Robot.Fire(3);
        }

        public void TurnUntilYouLock(ScannedRobotEvent enemy)
        {
            while (enemy.Bearing > Math.Abs(3))
            {
                if (enemy.Bearing < 0)
                {
                    Robot.TurnRight(5);
                    Robot.TurnRadarRight(5);
                    Robot.Scan();
                }
                else
                {
                    Robot.TurnLeft(5);
                    Robot.TurnRadarLeft(5);
                    Robot.Scan();
                }
                TurnUntilYouLock(enemy);
            }
        }
        public void SpinUntilYouSeeSomething()
        {
            while (true)
            {
                Robot.TurnRadarRight(5);
                Robot.Scan();
            }
        }
    }
}