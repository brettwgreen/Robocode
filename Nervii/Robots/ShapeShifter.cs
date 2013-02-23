using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Robocode;

namespace Nervii
{
    public class ShapeShifter : AdvancedRobot
    {
        private List<IRobotBehavior> robotBehaviors = new List<IRobotBehavior>();
        private int currentBehaviorIndex = 0;
        private IRobotBehavior currentBehavior;
        private static int kills = 0;
        private static int deaths = 0;

        public ShapeShifter()
        {
            robotBehaviors.Add(new ShockWaveBehavior(this));
            robotBehaviors.Add(new MegaCrasherBehavior(this));
            robotBehaviors.Add(new RambotBehavior(this));
            currentBehavior = robotBehaviors[currentBehaviorIndex];
        }
      
        private void SetNextBehavior()
        {
            if (kills < deaths)
            {
                if (currentBehaviorIndex == robotBehaviors.Count() - 1)
                {
                    currentBehaviorIndex = 0;
                }
                else
                {
                    currentBehaviorIndex++;
                }
                currentBehavior = robotBehaviors[currentBehaviorIndex];
                Console.WriteLine("Switching to " + currentBehavior.BehaviorName);
            }
        }

        private void SetColors()
        {
            this.BodyColor = Color.Black;
            this.GunColor = Color.Black;
            this.RadarColor = Color.Black;
            this.ScanColor = Color.Black;
        }

        public override void Run()
        {
            Console.WriteLine("Starting up");
            currentBehavior.Setup();
            SetColors();

            while (true)
            {
                currentBehavior.RunBehavior();
            }
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            currentBehavior.OnHitByBulletBehavior(evnt);
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            currentBehavior.OnHitRobotBehavior(evnt);
        }

        public override void OnScannedRobot(ScannedRobotEvent enemy)
        {
            currentBehavior.OnScannedRobotBehavior(enemy);
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            currentBehavior.OnHitWallBehavior(evnt);
        }

        public override void OnWin(WinEvent evnt)
        {
            currentBehavior.OnWinBehavior(evnt);
        }

        public override void OnBulletHit(BulletHitEvent evnt)
        {
            if (evnt.VictimEnergy == 0)
            {
                kills++;
                Console.WriteLine(String.Format("Deaths = {0}, Kills = {1}", deaths, kills));
            }
        }

        public override void OnDeath(DeathEvent evnt)
        {
            deaths++;
            Console.WriteLine(String.Format("Deaths = {0}, Kills = {1}", deaths, kills));
            SetNextBehavior();
        }

        public override void OnKeyPressed(KeyEvent e)
        {
            //currentBehaviorIndex++;
        }

    }
}
