using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace Nervii
{
    public class SpinBotBehavior : IRobotBehavior
    {
        public AdvancedRobot Robot { get; set; }
        public string BehaviorName { get { return "SpinBot"; } }

        public SpinBotBehavior(AdvancedRobot robot)
        {
            Robot = robot;
        }

        public void Setup()
        {
            Robot.IsAdjustGunForRobotTurn = false;
        }

        public void RunBehavior()
        {
            // Tell the game that when we take move,
            // we'll also want to turn right... a lot.
            Robot.SetTurnRight(10000);
            // Limit our speed to 5
            Robot.MaxVelocity = 5;
            // Start moving (and turning)
            Robot.Ahead(10000);
            // Repeat.
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            Robot.Fire(3);
        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            
        }

        public void OnHitWallBehavior(HitWallEvent evnt)
        {

        }


        public void OnHitRobotBehavior(HitRobotEvent evnt)
        {
            if (evnt.Bearing > -10 && evnt.Bearing < 10)
            {
                Robot.Fire(3);
            }
            if (evnt.IsMyFault)
            {
                Robot.TurnRight(10);
            }            
        }

        public void OnWinBehavior(WinEvent evnt) 
        {
            Robot.TurnLeft(25);
            for (int i = 0; i < 10; i++)
            {
                Robot.TurnRight(50);
                Robot.TurnLeft(50);
            }
            Console.WriteLine("All your bots are belong to us");
        }

    }
}
