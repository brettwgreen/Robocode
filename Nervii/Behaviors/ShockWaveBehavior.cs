using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.Util;

namespace Nervii
{
    public class ShockWaveBehavior : IRobotBehavior
    {
        public AdvancedRobot Robot { get; set; }
        public string BehaviorName { get { return "Shockwave"; } }
        double wallTolerance = 50;
        bool turnRight = true;

        public ShockWaveBehavior(AdvancedRobot robot)
        {
            Robot = robot;
        }

        private bool TooCloseToWall()
        {
            var tooClose = Robot.X < wallTolerance
                || Robot.Y < wallTolerance
                || Robot.X > (Robot.BattleFieldWidth - wallTolerance)
                || Robot.Y > (Robot.BattleFieldHeight - wallTolerance);
            Console.WriteLine("Too Close To Wall = " + tooClose.ToString());
            return tooClose;
        }

        public void Setup()
        {
            Robot.IsAdjustGunForRobotTurn = true;
            Robot.BodyColor = Color.DarkOliveGreen;
            Robot.GunColor = Color.DarkRed;
            Robot.RadarColor = Color.DarkRed;
            PointToCenter();

        }


        private void PointToCenter()
        {
            double angle = Helpers.GetAngle(Robot.X, Robot.Y, Robot.BattleFieldWidth / 2, Robot.BattleFieldHeight / 2);
            double turn = angle - Robot.Heading;
            Console.WriteLine("Turning to center angle " + turn.ToString());
            Robot.ClearAllEvents();
            Robot.SetTurnRight(Utils.NormalRelativeAngleDegrees(turn));
            Robot.SetTurnGunRight(Utils.NormalRelativeAngleDegrees(turn));
            Robot.WaitFor(new TurnCompleteCondition(Robot));
        }

        public void RunBehavior()
        {
            //GoToNearestStartingPoint();
            Robot.SetTurnGunRight(double.MaxValue);
            Robot.SetAhead(10000);
            Robot.SetTurnRight(90);
            Robot.WaitFor(new TurnCompleteCondition(Robot));
            Robot.SetTurnLeft(90);
            Robot.WaitFor(new TurnCompleteCondition(Robot));
            Robot.SetTurnLeft(45);
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            if (Robot.IsEnemy(enemy.Name))
            {
                Robot.ClearAllEvents();
                bool headOn = false;
                bool tracking = false;
                double relativePath = 0;
                bool movingHardRight = false;
                bool movingHardLeft = false;

                if (enemy.Heading > Robot.Heading)
                {
                    relativePath = enemy.Heading - Robot.Heading;
                }
                else
                {
                    relativePath = Robot.Heading - enemy.Heading;
                }
                if (relativePath <= 2)
                {
                    tracking = true;
                }
                else if (relativePath >= 178 && relativePath <= 182)
                {
                    tracking = true;
                    headOn = true;
                }
                //Console.WriteLine("Enemy Heading = " + enemy.Heading.ToString());
                //Console.WriteLine("My Heading = " + Robot.Heading.ToString());
                //Console.WriteLine("Relative Path = " + relativePath.ToString());
                //Console.WriteLine("Distance = " + enemy.Distance.ToString());
                //Console.WriteLine("Velocity = " + enemy.Velocity.ToString());
                //Console.WriteLine("Tracking = " + tracking.ToString());
                //Console.WriteLine("GunHeat = " + Robot.GunHeat.ToString());

                double gunTurn = Utils.NormalRelativeAngleDegrees(enemy.Bearing + (Robot.Heading - Robot.RadarHeading));

                // To do: adjust for moving tank?
                Robot.TurnGunRight(gunTurn);
                Robot.WaitFor(new GunTurnCompleteCondition(Robot));

                if (Robot.GunHeat == 0)
                {
                    Console.WriteLine("Distance = " + enemy.Distance);
                    if ((enemy.Distance > 300 && enemy.Distance < 400))
                    {
                        Console.WriteLine("FIRE 1!!!");
                        Robot.SafeFire(1, enemy.Name);
                    }
                    else if (enemy.Distance > 150 && enemy.Distance <= 300)
                    {
                        Console.WriteLine("FIRE 2!!!");
                        Robot.SafeFire(2, enemy.Name);
                    }
                    else if (enemy.Distance <= 150)
                    {
                        Console.WriteLine("FIRE 3!!!");
                        Robot.SafeFire(3, enemy.Name);
                    }
                }
                if (enemy.Distance > 150)
                {
                    Console.WriteLine("I'm in hot pursuit!");
                    Robot.SetTurnRight(enemy.Bearing);
                    Robot.SetAhead(enemy.Distance / 2);
                }
                else if (enemy.Velocity < 1.0)
                {
                    Console.WriteLine("Punch and Move!");
                    if (turnRight)
                    {
                        Console.WriteLine("Circle Right");
                        Robot.SetTurnRight(Utils.NormalRelativeAngleDegrees(enemy.Bearing + 90));
                        Robot.SetTurnGunLeft(Utils.NormalRelativeAngleDegrees(enemy.Bearing - 90));
                    }
                    else
                    {
                        Console.WriteLine("Circle Left");
                        Robot.SetTurnLeft(Utils.NormalRelativeAngleDegrees(enemy.Bearing + 90));
                        Robot.SetTurnGunRight(Utils.NormalRelativeAngleDegrees(enemy.Bearing - 90));
                    }
                    Robot.SetAhead(enemy.Distance);
                }
            }
        }

        public void EvasiveManuevers(HitByBulletEvent evnt)
        {
            Robot.ClearAllEvents();
            Robot.SetBack(300);
            Robot.SetTurnLeft(90);
            Robot.SetTurnGunRight(90);
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
            turnRight = !turnRight;
            PointToCenter();
            Robot.SetAhead(500);
            Robot.SetTurnRight(45);
            //Robot.Stop();
            //PointToCenter();
            //Robot.Resume();
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            Robot.TurnGunLeft(double.MaxValue);
            Console.WriteLine("All your bots are belong to us");
        }

    }
}
