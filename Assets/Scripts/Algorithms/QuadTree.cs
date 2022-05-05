using System;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    public struct Point // for now keep it int, but we might have to switch to floats for more precision
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
    public struct Node<T>
    {
        public Node(T data, Point point)
        {
            Data = data;
            Point = point;
        }
        public Point Point { get; }
        public T Data { get; }
    }
    public class QuadTree<T>
    {
        public QuadTree(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BotRight = bottomRight;
        }
        public Point TopLeft { get; }
        public Point BotRight { get; }

        public Node<T>? Node { get;  private set;} // Details of the node.

        public QuadTree<T> TopLeftTree { get; private set;}
        public QuadTree<T> BotLeftTree { get; private set;}
        public QuadTree<T> TopRightTree { get; private set; }
        public QuadTree<T> BotRightTree { get; private set;}

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
            }

            var leftTree = (TopLeft.X + BotRight.X) / 2 >= node.Point.X;
            if (leftTree)
            {
                var topLeft = (TopLeft.X + BotRight.Y) / 2 >= node.Point.Y;
                if (topLeft)
                {
                    if (TopLeftTree is null)
                    {
                        TopLeftTree = new QuadTree<T>(
                            TopLeft,
                            new Point((TopLeft.X + BotRight.X) / 2, (TopLeft.Y + BotRight.Y) / 2)
                        );
                    }

                    TopLeftTree.Insert(node);
                }
                else
                {
                    if (BotLeftTree is null)
                    {
                        BotLeftTree = new QuadTree<T>(
                            new Point(TopLeft.X, (TopLeft.Y + BotRight.Y) / 2),
                            new Point((TopLeft.X + BotRight.X) / 2, BotRight.Y)
                            );
                    }

                    BotLeftTree.Insert(node);
                }
            }
            else
            {
                var topRight = (TopLeft.Y + BotRight.Y) / 2 >= node.Point.Y;
                if (topRight)
                {
                    if (TopRightTree is null)
                    {
                        TopRightTree = new QuadTree<T>(
                            new Point((TopLeft.X + BotRight.X) / 2, TopLeft.Y),
                            new Point(BotRight.X, (TopLeft.Y + BotRight.Y) / 2)
                            );
                    }

                    TopRightTree.Insert(node);
                }
                else
                {
                    if (BotRightTree is null)
                    {
                        BotRightTree = new QuadTree<T>(
                            new Point((TopLeft.X + BotRight.X) / 2, (TopLeft.Y + BotRight.Y) / 2),
                            BotRight
                        );
                    }

                    BotRightTree.Insert(node);
                }
            }
        }

        private bool InBoundary(Node<T> node) =>
            TopLeft.X <= node.Point.X && node.Point.X <= BotRight.X &&
            TopLeft.Y <= node.Point.Y && node.Point.Y <= BotRight.Y;

        public Node<T> FindNearest(Point p, (float d, Node<T> node) best, QuadTree<T> quadTree)
        {
            if (p.X < quadTree.TopLeft.X - best.d ||
                p.X > quadTree.BotRight.X + best.d ||
                p.Y < quadTree.TopLeft.X - best.d ||
                p.Y > quadTree.BotRight.Y - best.d)
            {
                //Exclude node if point is farther away than best distance in either axis
                return best.node;
            }

            if (quadTree.Node != null)
            {
                var dx = quadTree.Node.Value.Point.X - p.X;
                var dy = quadTree.Node.Value.Point.Y - p.Y;
                var delta = Mathf.Sqrt(dx * dx + dy * dy);
                best.d = delta;
                best.node = quadTree.Node.Value;
            }

            //We check if the kids is on the right or left, and then recurse the most likely candidates first.
            var right = (2 * p.X > quadTree.TopLeft.X + quadTree.BotRight.X);
            var top = (2 * p.Y > quadTree.TopLeft.Y + quadTree.BotRight.Y);

            return best.node;
        }

        private QuadTree<T>[] Trees => new [] { TopLeftTree, TopRightTree, BotLeftTree, BotRightTree };
    }
}
