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
            if (enemy.Distance < 300)
            {
                Robot.Ahead(enemy.Distance * 1.3);
            }
            else
            {
                RunBehavior();
            }
            Robot.Scan();

        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            var bearing = evnt.Bearing;
            var directionToFace = 0;
            var bearingMinus180 = bearing - 180;
            if (bearingMinus180 > 0)
            {
                Robot.TurnTo(bearingMinus180, Helpers.TurnType.Robot);
            }
            else
            {
                Robot.TurnTo(bearing + 180, Helpers.TurnType.Robot);
            }

            Robot.Ahead(500);
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
            RunBehavior();
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
    }
}