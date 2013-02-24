using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Robocode;

namespace Nervii
{
    public class MegaCrasherBehavior : IRobotBehavior
    {
        public Robot Robot { get; set; }
        public string BehaviorName { get { return "MegaCrasher"; } }

        public MegaCrasherBehavior(Robot robot)
        {
            Robot = robot;
        }

        public void Setup()
        {
            Robot.BodyColor = Color.Teal;
            Robot.GunColor = Color.Green;
            Robot.RadarColor = Color.Blue;
            Robot.BulletColor = Color.Teal;
            Robot.IsAdjustGunForRobotTurn = false;
        }

        public void RunBehavior()
        {
            double distance = 0;
            double middleX = Robot.BattleFieldWidth / 2;
            double middleY = Robot.BattleFieldHeight / 2;
            if (Robot.X < middleX && Robot.Y < middleY)
            {
                // I'm in the bottom-left
                Robot.TurnRight(90);
                distance = middleX - Robot.X;
            }
            else if (Robot.X < middleX && Robot.Y > middleY)
            {
                // I'm in the upper-left
                Robot.TurnRight(90);
                distance = middleX - Robot.X;
            }
            else if (Robot.X > middleX && Robot.Y < middleY)
            {
                // I'm in the bottom-right
                Robot.TurnLeft(90);
                distance = Robot.X - middleX;
            }
            else if (Robot.X > middleX && Robot.Y > middleY)
            {
                // I'm in the upper-right
                Robot.TurnLeft(90);
                distance = Robot.X - middleX;
            }
            while(true)
            {
                // Heading... which way am I facing
                Robot.Ahead(distance);
                if (Robot.Heading < 180)
                {
                    Robot.TurnRight(180 - Robot.Heading);
                }
                else if (Robot.Heading > 180)
                {
                    Robot.TurnLeft(Robot.Heading - 180);
                }
                Robot.Ahead(400);
                Robot.TurnRight(180);
                Robot.Ahead(400);
                Robot.TurnRight(90);
                Robot.Ahead(200);
             
                Robot.TurnRight(90);
                Robot.Ahead(400);
                Robot.TurnRight(180);
                Robot.Ahead(800);
                Robot.TurnRight(90);
                Robot.Ahead(400);
                Robot.TurnRight(90);
                Robot.Ahead(800);
                Robot.TurnRight(90);
                Robot.Ahead(800);
                Robot.TurnRight(90);
                Robot.Ahead(800);
                Robot.TurnRight(90);
                Robot.Ahead(400);

            }
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            // Shoot at stuff
            Robot.SafeFire(3, enemy.Name);
        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            //throw new NotImplementedException();
        }

        public void OnHitRobotBehavior(HitRobotEvent evnt)
        {
            //throw new NotImplementedException();
        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {
            //throw new NotImplementedException();
        }

        public void OnWinBehavior(WinEvent evnt)
        {
            //throw new NotImplementedException();
        }
    }
}

