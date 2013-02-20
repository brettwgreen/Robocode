using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace Nervii
{
    public class ShockWaveBehavior : IRobotBehavior
    {
        public AdvancedRobot Robot { get; set; }
        public string BehaviorName { get { return "Shockwave Bot"; } }
        private double wallTolerance = 150;

        public ShockWaveBehavior(AdvancedRobot robot)
        {
            Robot = robot;
        }

        public void Setup()
        {
            Robot.IsAdjustGunForRobotTurn = false;
            PointToCenter();
        }

        private bool TooCloseToWall()
        {
            var tooClose = Robot.X < wallTolerance
                || Robot.Y < wallTolerance
                || Robot.X > (Robot.BattleFieldWidth - wallTolerance)
                || Robot.Y > (Robot.BattleFieldHeight- wallTolerance);
            Console.WriteLine("Too Close To Wall = " + tooClose.ToString());
            return tooClose;
        }

        private void PointToCenter()
        {
            double angle = Helpers.GetAngle(Robot.X, Robot.Y, Robot.BattleFieldWidth / 2, Robot.BattleFieldHeight / 2);
            Robot.TurnTo(angle, Helpers.TurnType.Robot);
        }

        public void RunBehavior()
        {
            if (TooCloseToWall())
            {
                PointToCenter();
            }

            Robot.SetAhead(double.MaxValue);
            Robot.SetTurnRight(90);
            Robot.WaitFor(new TurnCompleteCondition(Robot));
            Robot.SetTurnLeft(180);
            Robot.WaitFor(new TurnCompleteCondition(Robot));
            Robot.SetTurnRight(360);

        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            double relativePath = Math.Abs(enemy.Heading - Robot.Heading);
            bool tracking = (relativePath <= 185 && relativePath >= 175) || (relativePath >= -5 && relativePath < 5);
            Console.WriteLine(relativePath);
            if ((enemy.Distance < 500 && tracking) || enemy.Distance < 150 || enemy.Velocity < 2.0)
            {
                Robot.Fire(3);
            }
            if (enemy.Distance > 100)
            {
                Robot.SetTurnRight(enemy.Bearing);
                Robot.SetAhead(enemy.Distance / 3);
            }
            else
            {
                Robot.SetTurnRight(enemy.Bearing*-1);
                Robot.SetBack(enemy.Distance / 2);
            }

        }

        public void EvasiveManuevers(HitByBulletEvent evnt)
        {
        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            EvasiveManuevers(evnt);
        }

        public void OnHitRobotBehavior(HitRobotEvent evnt)
        {

        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {
            Robot.Stop();
            PointToCenter();
            Robot.Resume();
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            Robot.Stop();
            Robot.ClearAllEvents();
            Robot.SetAhead(double.MaxValue);
            Robot.TurnRight(double.MaxValue);
            Robot.Execute();
            Console.WriteLine("All your bots are belong to us");
        }

    }
}
