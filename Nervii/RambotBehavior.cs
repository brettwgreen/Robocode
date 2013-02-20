using Robocode;
using System;

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
            throw new NotImplementedException();
        }

        public void RunBehavior()
        {
            throw new NotImplementedException();
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            TurnUntilYouLock(enemy);
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
            throw new NotImplementedException();
        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {
            Robot.TurnLeft(180);
            Robot.Ahead(500);
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            throw new NotImplementedException();
        }

        public void TurnUntilYouLock(ScannedRobotEvent enemy)
        {
            while (enemy.Bearing > Math.Abs(0))
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
    }
}