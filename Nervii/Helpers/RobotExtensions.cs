using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace Nervii
{
    public static class RobotExtensions
    {
        private static void Turn(this Robot robot, Helpers.TurnType type, Helpers.TurnDirection direction, double angle)
        {
            //angle = Utils.NormalRelativeAngleDegrees(angle);

            if (type == Helpers.TurnType.Gun)
            {
                if (direction == Helpers.TurnDirection.Left)
                {
                    robot.TurnGunLeft(angle);
                }
                else
                {
                    robot.TurnGunRight(angle);
                }
            }
            if (type == Helpers.TurnType.Radar)
            {
                if (direction == Helpers.TurnDirection.Left)
                {
                    robot.TurnRadarLeft(angle);
                }
                else
                {
                    robot.TurnRadarRight(angle);
                }
            }
            if (type == Helpers.TurnType.Robot)
            {
                if (direction == Helpers.TurnDirection.Left)
                {
                    robot.TurnLeft(angle);
                }
                else
                {
                    robot.TurnRight(angle);
                }
            }
        }

        public static void TurnTo(this Robot robot, double angle, Helpers.TurnType type)
        {
            double heading = 0;
            if (type == Helpers.TurnType.Gun)
            {
                heading = robot.GunHeading;
            }
            else if (type == Helpers.TurnType.Radar)
            {
                heading = robot.RadarHeading;
            }
            else
            {
                heading = robot.Heading;
            }
            if (heading > angle)
            {
                if ((heading - angle) < 180)
                {
                    Turn(robot, type, Helpers.TurnDirection.Left, heading - angle);
                }
                else
                {
                    Turn(robot, type, Helpers.TurnDirection.Right, (360 - heading) + angle);
                }
            }
            else
            {
                if ((angle - heading) < 180)
                {
                    Turn(robot, type, Helpers.TurnDirection.Right, angle - heading);
                }
                else
                {
                    Turn(robot, type, Helpers.TurnDirection.Left, (360 - angle) + heading);
                }
            }
            //Console.WriteLine(String.Format("Turning {0} {1} from heading {2}", Enum.GetName(type.GetType(), type), angle, robot.Heading.ToString()));
        }

        public static bool IsEnemy(this Robot robot, string enemyName)
        {
            return !enemyName.ToLower().Contains("nervii") || robot.Others == 1;
        }

        public static void SafeFire(this Robot robot, double power, string enemyName) {
            //Console.Write("Enemy == " + enemyName);
            if (!enemyName.ToLower().Contains("nervii") || robot.Others == 1)
            {
                robot.Fire(power);
            }
        }

        public static void MoveToPoint(this Robot robot, double destX, double destY)
        {
            var angle = Helpers.GetAngle(robot.X, robot.Y, destX, destY);
            var distance = Helpers.GetDistance(robot.X, robot.Y, destX, destY);
            TurnTo(robot, angle, Helpers.TurnType.Robot);
            robot.Ahead(distance);
        }

    }
}
