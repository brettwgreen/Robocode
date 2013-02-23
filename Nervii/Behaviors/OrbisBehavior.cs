using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using Robocode.RobotInterfaces;

namespace Nervii
{
    public class OrbisBehavior : IRobotBehavior
    {
        public AdvancedRobot Robot { get; set; }
        public string BehaviorName { get { return "Orbis"; } }

        private Location[] Walls = new Location[4];
        private int currentWall = 0;
        private double centerX;
        private double centerY;
        
        public OrbisBehavior(AdvancedRobot robot)
        {
            Robot = robot;
        }

        public void Setup()
        {

            this.SetWallLocations();

            Robot.IsAdjustGunForRobotTurn = true;

            centerX = Robot.BattleFieldWidth / 2;
            centerY = Robot.BattleFieldHeight / 2;
        }

        public void RunBehavior()
        {
            Console.WriteLine("Run loop");
            double rotation = 0;
            if (currentWall == 0)
            {
                rotation = Helpers.GetAngle(0, 0, centerX, centerY);
            }
            else if (currentWall == 1)
            {
                rotation = Helpers.GetAngle(0, Robot.BattleFieldHeight, centerX, centerY);
            }
            else if (currentWall == 2)
            {
                rotation = Helpers.GetAngle(Robot.BattleFieldWidth, Robot.BattleFieldHeight, centerX, centerY);
            }
            else if (currentWall == 3)
            {
                rotation = Helpers.GetAngle(Robot.BattleFieldWidth, 0, centerX, centerY);
            }
            Robot.TurnTo(rotation, Helpers.TurnType.Gun);
            Console.WriteLine("Rotation: " + rotation.ToString());

            Robot.MoveToPoint(Walls[currentWall].X, Walls[currentWall].Y);

            if (currentWall == 3)
            {
                currentWall = 0;
            }
            else
            {
                currentWall++;
            }
        }

        public void OnScannedRobotBehavior(ScannedRobotEvent enemy)
        {
            Console.WriteLine("On scanned robot");
            Robot.Stop();
            double fireStrength = 3;
            if (enemy.Distance < 200)
            {
                Robot.Fire(fireStrength);
                Robot.Scan();
            }
            if (enemy.Heading > Robot.Heading)
            {
                Robot.TurnTo((enemy.Heading - Robot.Heading) + enemy.Velocity * 5, Helpers.TurnType.Robot);
            }
            else
            {
                Robot.TurnTo((Robot.Heading - enemy.Heading) + enemy.Velocity * 5, Helpers.TurnType.Robot);
            }
            if (enemy.Distance > 100)
            {
                Robot.Ahead(enemy.Distance / 2);
            }
            Robot.Scan();
            //this.SetTurnGunRight(10);
            //this.SetTurnGunRight(20);

            //Scan();
            Robot.Resume();
        }

        public void OnHitByBulletBehavior(HitByBulletEvent evnt)
        {
            
        }

        public void OnHitRobotBehavior(HitRobotEvent evnt)
        {
            
        }
        public void OnHitWallBehavior(HitWallEvent evnt)
        {

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


        #region "private methods"

        private void SetWallLocations()
        {
            //left
            Walls[0] = new Location(40, Robot.BattleFieldHeight / 2);
            //top
            Walls[1] = new Location(Robot.BattleFieldWidth / 2, Robot.BattleFieldHeight - 40);
            //right
            Walls[2] = new Location(Robot.BattleFieldWidth - 40, Robot.BattleFieldHeight / 2);
            //bottom
            Walls[3] = new Location(Robot.BattleFieldWidth / 2, 40);
        }
        #endregion

    }
}
