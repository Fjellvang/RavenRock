using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Algorithms;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class QuadTreeTests
    {
        QuadTree<int> baseTree;
        Point center = new Point(500, 500);
        List<Point> pointsSorted;
        [SetUp]
        public void Setup()
        {
            baseTree = new QuadTree<int>(new Point(0, 1000), new Point(1000, 0));
            var points = new Dictionary<double, Point>();
            for (int i = 0; i < 1000; i++)
            {
                var (d, point) = GetPoint();

                while (points.ContainsKey(d))
                {
                    (d, point) = GetPoint();
                }

                points.Add(d, point);

                baseTree.Insert(new Node<int>(0, point));
            }

            pointsSorted = points.OrderBy(x => x.Key).Select(x => x.Value).ToList();
        }

        private (double d, Point p) GetPoint()
        {
            var x = Random.Range(0,1000);
            var y = Random.Range(0, 1000);
            var point = new Point(x, y);
            var d = center.DistanceTo(point);
            return (d, point);
        }

        [Test]
        public void BaseTree_TestNearestNodeFound()
        {
            var nearest = baseTree.FindNearest(center);

            Assert.That(nearest.node.Point, Is.EqualTo(pointsSorted.First()));
        }
        [Test]
        public void BaseTree_TestNearestNodeFoundForAll()
        {
            for (int i = 0; i < 1000; i++)
            {
                var nearest = baseTree.PopNearest(center);
                Assert.That(nearest.node.Point, Is.EqualTo(pointsSorted[i]));
            }
        }
    }
}
