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
        QuadTree<int> quadTree;
        Point center = new Point(500, 500);
        List<Point> pointsSorted;
        [SetUp]
        public void Setup()
        {
            quadTree = new QuadTree<int>(new Point(0, 1000), new Point(1000, 0));
            var points = new Dictionary<double, Point>();
            for (int i = 0; i < 1000; i++)
            {
                var (d, point) = GetPoint();

                while (points.ContainsKey(d))
                {
                    (d, point) = GetPoint();
                }

                points.Add(d, point);

                quadTree.Insert(new Node<int>(0, point));
            }

            pointsSorted = points.OrderBy(x => x.Key).Select(x => x.Value).ToList();
        }

        private (double d, Point p) GetPoint()
        {
            var x = Random.Range(0, 1000);
            var y = Random.Range(0, 1000);
            var point = new Point(x, y);
            var d = center.DistanceTo(point);
            return (d, point);
        }

        [Test]
        public void BaseTree_TestNearestNodeFound()
        {
            var nearest = quadTree.FindNearest(center);

            Assert.That(nearest.node.Point, Is.EqualTo(pointsSorted.First()));
        }
        [Test]
        public void BaseTree_TestNearestNodeFoundForAll()
        {
            for (int i = 0; i < 1000; i++)
            {
                var nearest = quadTree.PopNearest(center);
                Assert.That(nearest.node.Point, Is.EqualTo(pointsSorted[i]));
            }
        }
        [Test]
        public void QuadTree_SearchInQuadrant()
        {
            var ps = pointsSorted.Where(x => x.X >- 500 && x.Y >= 500).ToList(); // all points in upperRight quadrant

            List<Node<int>> pointsFound = quadTree.FindInQuadrant(new Point(500, 1000), new Point(1000, 500));

            var expected = ps.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            var result = pointsFound.OrderBy(x => x.Point.X).ThenBy(x => x.Point.Y).Select(x => x.Point).ToList();

            Assert.That(result, Is.All.EquivalentTo(expected));
        }
        [Test]
        public void QuadTree_SearchInQuadrant_Middle()
        {
            var topLeft = new Point(250, 750);
            var bottomRight = new Point(750, 250);
            var ps = pointsSorted
                .Where(x => x.LiesWithin(topLeft, bottomRight))
                .ToList(); 

            List<Node<int>> pointsFound = quadTree.FindInQuadrant(topLeft, bottomRight);

            var expected = ps.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            var result = pointsFound.OrderBy(x => x.Point.X).ThenBy(x => x.Point.Y).Select(x => x.Point).ToList();

            Assert.That(result, Is.All.EquivalentTo(expected));
        }
    }
}
