using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Nervii;
using System.Drawing;
using Robocode.Util;

namespace Nervii.Tests
{
    [TestFixture]
    public class RobotTests
    {
        [Test]
        public void TestGetDistance()
        {
            var dist = OrbisBot.GetDistance(new Point(0, 0), new Point(10, 0));
            Assert.AreEqual(10, dist);
        
            dist = OrbisBot.GetDistance(new Point(0, 0), new Point(100, 100));
            Assert.AreEqual(true, Utils.IsNear(dist, 141.421356));
        }

        [Test]
        public void TestAngles()
        {
            var angle = OrbisBot.GetAngle(new Point(0, 0), new Point(10, 0));
            //Assert.AreEqual(90, angle);
            angle = OrbisBot.GetAngle(new Point(0, 0), new Point(10, 10));
            Assert.AreEqual(true, Utils.IsNear(angle, 45));
        }
    }
}
