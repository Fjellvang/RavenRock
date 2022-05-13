using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Algorithms;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PointTests
    {
        [TestCase(0,1000,1000,0,500,1000)]
        [TestCase(0,1000,1000,0,1000,500)]

        [TestCase(500,1000, 1000, 500, 531,1000)]
        [TestCase(500,1000, 1000, 500, 562,968)]
        public void LiesWithin_ReturnsTrue_WhenWithin(int topLeftx, int topleftY, int botRightx, int botRightY, int x, int y)
        {
            var topLeft = new Point(topLeftx, topleftY);
            var botRight = new Point(botRightx, botRightY);

            var sut = new Point(x,y);

            Assert.That(sut.LiesWithin(topLeft, botRight), Is.True);
        }


        [TestCaseSource(nameof(OverlapTestCase))]
        public void OverLaps_ReturnsTrue_WhenOverlapping(Point topleft0, Point bottomRight0, Point topLeft1, Point bottomRight1)
        {
            Assert.That(Point.Overlaps(topleft0, bottomRight0, topLeft1, bottomRight1), Is.True);    
        }

        public static object[] OverlapTestCase =
        {
            new object[] {new Point(10,0), new Point(0,10), new Point(5,0),new Point(5,1) }
        };
    }
}
