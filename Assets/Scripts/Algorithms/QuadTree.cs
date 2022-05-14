using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    public struct Point // for now keep it int, but we might have to switch to floats for more precision
    {
        public Point(Vector3 vector3)
        {
            X = Mathf.RoundToInt(vector3.x);
            Y = Mathf.RoundToInt(vector3.y);
        }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public float DistanceTo(Point p)
        {
            var dx = p.X - X;
            var dY = p.Y - Y;
            return Mathf.Sqrt(dx * dx + dY * dY);
        }

        public bool LiesWithin(Point topLeft, Point bottomRight)
        {
            return X <= bottomRight.X && topLeft.X <= X && Y <= topLeft.Y && bottomRight.Y <= Y;
        }

        /// <summary>
        /// returns true if the rect defined by <paramref name="topleft0"/> && <paramref name="bottomRight0"/> overlaps the rect defined by <paramref name="topLeft1"/> && <paramref name="bottomRight1"/>
        /// </summary>
        /// <param name="topleft0"></param>
        /// <param name="bottomRight0"></param>
        /// <param name="topLeft1"></param>
        /// <param name="bottomRight1"></param>
        /// <returns></returns>
        public static bool Overlaps(Point topLeft0, Point bottomRight0, Point topLeft1, Point bottomRight1)
        {
            var overlapsX = !(topLeft0.X > bottomRight1.X || topLeft1.X > bottomRight0.X);
            var overlapsY = !(bottomRight0.Y > topLeft1.Y || bottomRight1.Y > topLeft0.Y);

            return overlapsX && overlapsY;
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
        public override bool Equals(object obj)
        {
            if (obj is Point p)
            {
                return p.X == X && p.Y == Y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
    public class Node<T> // marked as class so i easily can mark them used. This might be less optimal than removing from tree. TEST
    {
        public Node(T data, Point point)
        {
            Data = data;
            Point = point;
        }
        public Point Point { get; }
        public T Data { get; }
        public QuadTree<T> Tree { get; set; } // the tree which the node is stored in.
    }
    public class QuadTree<T>
    {
        public QuadTree(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BotRight = bottomRight;
            trees = new QuadTree<T>[4];
        }

        private QuadTree(Point topLeft, Point bottomRight, QuadTree<T> parent) :this(topLeft, bottomRight)
        {
            this.parent = parent;
        }

        public Point TopLeft { get; }
        public Point BotRight { get; }

        public Node<T> Node { get;  private set;} // Details of the node.

        public void Insert(Node<T> node)
        {
            if (!InBoundary(node))
            {
                return;
            }

            if (Mathf.Abs(TopLeft.X - BotRight.X) <= 1 && 
                Mathf.Abs(TopLeft.Y - BotRight.Y) <= 1)
            {
                if (Node is null)
                {
                    Node = node;
                }
                return;
            }

            var leftTree = (TopLeft.X + BotRight.X) / 2 >= node.Point.X;
            if (leftTree)
            {
                var topLeft = (TopLeft.Y + BotRight.Y) / 2 >= node.Point.Y;
                if (!topLeft)
                {
                    if (trees[0] is null)
                    {
                        trees[0] = new QuadTree<T>(
                            TopLeft,
                            new Point((TopLeft.X + BotRight.X) / 2, (TopLeft.Y + BotRight.Y) / 2),
                            this
                        );
                    }

                    trees[0].Insert(node);
                }
                else
                {
                    if (trees[2] is null)
                    {
                        trees[2] = new QuadTree<T>(
                            new Point(TopLeft.X, (TopLeft.Y + BotRight.Y) / 2),
                            new Point((TopLeft.X + BotRight.X) / 2, BotRight.Y),
                            this
                            );
                    }

                    trees[2].Insert(node);
                }
            }
            else
            {
                var topRight = (TopLeft.Y + BotRight.Y) / 2 >= node.Point.Y;
                if (!topRight)
                {
                    if (trees[1] is null)
                    {
                        trees[1] = new QuadTree<T>(
                            new Point((TopLeft.X + BotRight.X) / 2, TopLeft.Y),
                            new Point(BotRight.X, (TopLeft.Y + BotRight.Y) / 2),
                            this
                            );
                    }

                    trees[1].Insert(node);
                }
                else
                {
                    if (trees[3] is null)
                    {
                        trees[3] = new QuadTree<T>(
                            new Point((TopLeft.X + BotRight.X) / 2, (TopLeft.Y + BotRight.Y) / 2),
                            BotRight,
                            this
                        );
                    }

                    trees[3].Insert(node);
                }
            }
        }

        public List<Node<T>> FindInQuadrant(Point topLeft, Point botRight)
        {
            var result = new List<Node<T>>();
            FindInQuadrant(topLeft, botRight, result);
            return result;
        }
        private void FindInQuadrant(Point topLeft, Point botRight, List<Node<T>> result)
        {
            //var result = new List<Node<T>>();

            var outOfBounds = !Point.Overlaps(TopLeft, BotRight, topLeft, botRight);
            if (outOfBounds) 
            {
                return;
            }

            if (Node != null && Node.Point.LiesWithin(topLeft, botRight))
            {
                result.Add(Node);
                return;
            }

            for (int i = 0; i < trees.Length; i++)
            {
                if (trees[i] != null)
                {
                    trees[i].FindInQuadrant(topLeft, botRight, result);
                    //result.AddRange(trees[i].FindInQuadrant(topLeft, botRight, result));
                }
            }
        }

        private bool InBoundary(Node<T> node) =>
            TopLeft.X <= node.Point.X && node.Point.X <= BotRight.X &&
            TopLeft.Y >= node.Point.Y && node.Point.Y >= BotRight.Y;

        /// <summary>
        /// Finds the nearest node to <paramref name="p"/> and removes it from the tree
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public (float d, Node<T> node) PopNearest(Point p)
        {
            var best = (float.MaxValue, default(Node<T>));
            return PopNearest(p, best);
        }
        private (float d, Node<T> node) PopNearest(Point p, (float d, Node<T> node) best)
        {
            var result = FindNearest(p, best);
            if (result.node == null)
            {
                return result;
            }
            result.node.Tree.RemoveNode();
            return result;
        }
        private void RemoveNode()
        {
            Node = null;
            this.parent.RemoveTree();
        }
        private void RemoveTree()
        {
            if (Node != null)
            {
                return;
            }
            for (int i = 0; i < trees.Length; i++)
            {
                var t = trees[i];
                if (t != null && t.IsDeleteable())
                {
                    trees[i] = null;
                }
            }
            if (this.parent != null && this.parent.IsDeleteable())
            {
                this.parent.RemoveTree();
            }
        }
        private bool IsDeleteable()
        {
            if (Node != null)
            {
                return false;
            }
            for (int i = 0; i < trees.Length; i++)
            {
                if (trees[i] != null)
                {
                    return trees[i].IsDeleteable();
                }
            }
            return true;
        }
        public (float d, Node<T> node) FindNearestInQuadrant(Point toFind, Point topLeft, Point botRight)
        {
            return FindNearestInQuadrant(toFind, topLeft, botRight, (float.MaxValue, default(Node<T>)));
        }

        private (float d, Node<T> node) FindNearestInQuadrant(Point toFind, Point topLeft, Point botRight, (float d, Node<T> node) best)
        {
            if (toFind.X < TopLeft.X - best.d ||
                toFind.X > BotRight.X + best.d ||
                toFind.Y < BotRight.Y - best.d ||
                toFind.Y > TopLeft.Y + best.d)
            {
                //Exclude node if point is farther away than best distance in either axis
                return best;
            }

            var outOfBounds = !Point.Overlaps(TopLeft, BotRight, topLeft, botRight);
            if (outOfBounds) 
            {
                return best;
            }

            if (Node != null && Node.Point.LiesWithin(topLeft, botRight))
            {
                var delta = toFind.DistanceTo(Node.Point);
                if (delta < best.d)
                {
                    best.d = delta;
                    best.node = Node;
                    best.node.Tree = this;
                }
            }

            //We check if the kids is on the right or left, and then recurse the most likely candidates first.
            var right = (2 * toFind.X > TopLeft.X + BotRight.X) ? 1 : 0;
            var top = (2 * toFind.Y > TopLeft.Y + BotRight.Y) ? 1 : 0;

            var index0 = (1 - top) * 2 + right;         // if top & right  = (1 - 1) * 2 + 1        = 1
            var index1 = top * 2 + right;               // if top & right  = 1 * 2 + 1              = 3 // Hence on topright we will check, Topright,
            var index2 = (1 - top) * 2 + (1 - right);   // if top & right  = (1 - 1) * 2 + (1-1)    = 0
            var index3 = top * 2 + (1 - right);           // if top  & right = 1*2 + (1-1)            = 2

            if (trees[index0] != null)
            {
                best = trees[index0].FindNearestInQuadrant(toFind, topLeft, botRight, best);
            }
            if (trees[index1] != null)
            {
                best = trees[index1].FindNearestInQuadrant(toFind, topLeft, botRight, best);
            }
            if (trees[index2] != null)
            {
                best = trees[index2].FindNearestInQuadrant(toFind, topLeft, botRight, best);
            }
            if (trees[index3] != null)
            {
                best = trees[index3].FindNearestInQuadrant(toFind, topLeft, botRight, best);
            }

            return best;

        }
        public (float d, Node<T> node) FindNearest(Point p)
        {
            var best = (float.MaxValue, default(Node<T>));
            return FindNearest(p, best);
        }

        private (float d, Node<T> node) FindNearest(Point p, (float d, Node<T> node) best)
        {
            if (p.X < TopLeft.X - best.d ||
                p.X > BotRight.X + best.d ||
                p.Y < BotRight.Y - best.d ||
                p.Y > TopLeft.Y + best.d)
            {
                //Exclude node if point is farther away than best distance in either axis
                return best;
            }

            if (Node != null)
            {
                var delta = p.DistanceTo(Node.Point);
                if (delta < best.d)
                {
                    best.d = delta;
                    best.node = Node;
                    best.node.Tree = this;
                }
            }

            //We check if the kids is on the right or left, and then recurse the most likely candidates first.
            var right = (2 * p.X > TopLeft.X + BotRight.X) ? 1 : 0;
            var top = (2 * p.Y > TopLeft.Y + BotRight.Y) ? 1 : 0;

            var index0 = (1 - top) * 2 + right;         // if top & right  = (1 - 1) * 2 + 1        = 1
            var index1 = top * 2 + right;               // if top & right  = 1 * 2 + 1              = 3 // Hence on topright we will check, Topright,
            var index2 = (1 - top) * 2 + (1 - right);   // if top & right  = (1 - 1) * 2 + (1-1)    = 0
            var index3 = top * 2 + (1-right);           // if top  & right = 1*2 + (1-1)            = 2

            if (trees[index0] != null)
            {
                best = trees[index0].FindNearest(p, best);
            }
            if (trees[index1] != null)
            {
                best = trees[index1].FindNearest(p, best);
            }
            if (trees[index2] != null)
            {
                best = trees[index2].FindNearest(p, best);
            }
            if (trees[index3] != null)
            {
                best = trees[index3].FindNearest(p, best);
            }

            return best;
        }

        private QuadTree<T>[] trees;
        private readonly QuadTree<T> parent;

        public List<Node<T>> GetNodes()
        {
            var list = new List<Node<T>>();
            if (Node != null)
            {
                list.Add(Node);
            }
            if (trees[0] != null)
            {
                 list.AddRange(trees[0].GetNodes());
            }

            if (trees[1] != null)
            {
                list.AddRange(trees[1].GetNodes());
            }

            if (trees[2] != null)
            {
                list.AddRange(trees[2].GetNodes());
            }

            if (trees[3] != null)
            {
                list.AddRange(trees[3].GetNodes());
            }

            return list;
        }
    }
}
