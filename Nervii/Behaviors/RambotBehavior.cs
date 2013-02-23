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
            Robot.SetColors(Color.Yellow, Color.Fuchsia, Color.ForestGreen);
        }

        public void RunBehavior()
        {

            //SpinUntilYouSeeSomething();
            var rand = new Random();
            var height = Math.Round(Robot.BattleFieldHeight * .5);
            var width = Math.Round(Robot.BattleFieldWidth * .5);

            var randomHeight = rand.Next(100, (int)height);
            var randomWidth = rand.Next(100, (int)width);

            Robot.MoveToPoint(randomWidth, randomHeight);
            Robot.Scan();
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            Robot.ClearAllEvents();
            Robot.Ahead(enemy.Distance*1.3);
            Robot.Scan();
        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            Robot.TurnTo(evnt.Heading, Helpers.TurnType.Robot);
            Robot.Ahead(50);
            Robot.Scan();
            Robot.TurnLeft(15);
            Robot.Scan();
            Robot.Ahead(50);
            Robot.Scan();
            Robot.TurnRight(15);
            Robot.Scan();
            Robot.Ahead(50);
            Robot.Scan();
        }

        public void OnHitRobotBehavior(HitRobotEvent evnt)
        {
            if (Robot.GunHeat == 0) { Robot.Fire(3); }
            Robot.Back(100);
            if (Robot.GunHeat == 0) { Robot.Fire(3); }
            Robot.Scan();
        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {
            Robot.TurnLeft(180);
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            //The Sprinker
            for (var j = 5; j < 1000; j++)
            {
                Robot.TurnLeft(j);
                Robot.TurnRight(j * 1.5);
            }
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
            
                Robot.TurnRadarRight(5);
                Robot.Scan();
                Robot.Ahead(25);
                Robot.TurnRight(5);
            Robot.Scan();
                Robot.Ahead(25);
            Robot.Scan();
            SpinUntilYouSeeSomething();
            
        }
    }
}