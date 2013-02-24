using System.Drawing;
using Robocode;
using System;
using Nervii;

namespace Nervii
{
    public class RambotBehavior : IRobotBehavior
    {
        public Robot Robot { get; set; }
        public string BehaviorName { get { return "Rambot"; } }
        public Random rand;
        double height = 0.0;
        double width = 0.0;

        public RambotBehavior(Robot robot)
        {
            Robot = robot;
            rand = new Random(872349872);
            height = Math.Round(Robot.BattleFieldHeight);
            width = Math.Round(Robot.BattleFieldWidth);
        }

        public void Setup()
        {
            Robot.SetColors(Color.Yellow, Color.Fuchsia, Color.ForestGreen);
        }

        public void RunBehavior()
        {
            var randomHeight = rand.Next(100, (int)height - 100);
            var randomWidth = rand.Next(100, (int)width - 100);
            Robot.MoveToPoint(randomWidth, randomHeight);
            Console.WriteLine("Height: " + randomHeight + " Width: " + randomWidth);
            Robot.Scan();
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
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
            if (Robot.GunHeat == 0) { Robot.SafeFire(3, evnt.Name); }
            Robot.Back(100);
            if (Robot.GunHeat == 0) { Robot.SafeFire(3, evnt.Name); }
            Robot.Scan();
        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {
            RunBehavior();
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            //The Sprinkler
            for (var j = 5; j < 1000; j++)
            {
                Robot.TurnLeft(j);
                Robot.TurnRight(j * 1.5);
            }
        }
    }
}