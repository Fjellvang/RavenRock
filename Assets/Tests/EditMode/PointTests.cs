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


        [TestCaseSource(nameof(OverLaps))]
        public void OverLaps_ReturnsTrue_WhenOverlapping(Point topleft0, Point bottomRight0, Point topLeft1, Point bottomRight1)
        {
            Assert.That(Point.Overlaps(topleft0, bottomRight0, topLeft1, bottomRight1), Is.True);    
        }
        [TestCaseSource(nameof(DoesntOverLap))]
        public void OverLaps_ReturnsFalse_WhenNotOverlapping(Point topleft0, Point bottomRight0, Point topLeft1, Point bottomRight1)
        {
            Assert.That(Point.Overlaps(topleft0, bottomRight0, topLeft1, bottomRight1), Is.False);    
        }

        public static object[] OverLaps =
        {
            new object[] {new Point(0,2), new Point(2,0), new Point(1,0),new Point(0,1) },
            new object[] {new Point(0,3), new Point(3,0), new Point(3, 2),new Point(4,1) },
            new object[] {new Point(0,3), new Point(3,0), new Point(1, 4),new Point(2,3) },
            new object[] {new Point(0,3), new Point(3,0), new Point(-1, 2),new Point(0,1) },
            new object[] {new Point(0,3), new Point(3,0), new Point(1, 0),new Point(2,-1) },
            new object[] {new Point(515,1000), new Point(531,984), new Point(500, 1000),new Point(1000,500) },
        };
        public static object[] DoesntOverLap =
        {
            new object[] {new Point(0,1), new Point(1,0), new Point(0,3),new Point(1,2) },
            new object[] {new Point(0,1), new Point(1,0), new Point(2,1),new Point(3,0) },
            new object[] {new Point(0,1), new Point(1,0), new Point(0,-1),new Point(1,-2) },
            new object[] {new Point(0,1), new Point(1,0), new Point(-2,1),new Point(-1,0) },
        };

        public static object[] DoesntOverLap2 
        {   get
            {
                List<object[]> returnValues = new List<object[]>();
                var topleft = new Point(0, 1);
                var botRight = new Point(1, 0);
                for (int i = 0; i < 365; i += 5)
                {
                    var degree = i * Mathf.Deg2Rad;
                    var cos = Mathf.Cos(degree);
                    var sin = Mathf.Sin(degree);

                    var newTopLeft = new Point((int)(cos * 5), (int)(sin * 6));
                    var newBotRight = new Point((int)(cos * 6), (int)(sin * 5));
                    returnValues.Add(new object[] { topleft, botRight, newTopLeft, newBotRight });
                }
                return returnValues.ToArray();
            } 
        }
    }
}
