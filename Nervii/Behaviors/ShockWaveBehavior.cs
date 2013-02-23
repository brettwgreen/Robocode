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

        public ShockWaveBehavior(AdvancedRobot robot)
        {
            Robot = robot;
        }

        public void Setup()
        {
            Robot.IsAdjustGunForRobotTurn = true;
            Robot.BodyColor = Color.DarkOliveGreen;
            Robot.GunColor = Color.DarkRed;
            Robot.RadarColor = Color.DarkRed;
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
            PointToCenter();
            Robot.SetTurnGunRight(double.MaxValue);
            Robot.SetAhead(10000);
            Robot.SetTurnRight(90);
            Robot.WaitFor(new TurnCompleteCondition(Robot));
            Robot.SetTurnLeft(180);
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
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
            if (relativePath <= 5)
            {
                tracking = true;
            }
            else if (relativePath >= 175 && relativePath <= 185)
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
                if (enemy.Distance > 400)
                {
                    Console.WriteLine("FIRE 1!!!");
                    Robot.Fire(1);
                }
                else if (enemy.Distance > 150)
                {
                    Console.WriteLine("FIRE 2!!!");
                    Robot.Fire(2);
                }
                else
                {
                    Console.WriteLine("FIRE 3!!!");
                    Robot.Fire(3);
                }
            }
            if (enemy.Distance > 100)
            {
                Console.WriteLine("I'm in hot pursuit!");
                Robot.SetTurnRight(enemy.Bearing);
                Robot.SetAhead(enemy.Distance / 3);
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
