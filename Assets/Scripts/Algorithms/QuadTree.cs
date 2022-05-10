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
    }
    public class Node<T> // marked as class so i easily can mark them used. This might be less optimal than removing from tree. TEST
    {
        public Node(T data, Point point)
        {
            Data = data;
            Point = point;
            Used = false;
        }
        public Point Point { get; }
        public T Data { get; }
        public bool Used { get; set; }
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

        public Node<T> Node { get;  private set;} // Details of the node.

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
                return;
            }

            var leftTree = (TopLeft.X + BotRight.X) / 2 >= node.Point.X;
            if (leftTree)
            {
                var topLeft = (TopLeft.Y + BotRight.Y) / 2 >= node.Point.Y;
                if (!topLeft)
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
                if (!topRight)
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
            TopLeft.Y >= node.Point.Y && node.Point.Y >= BotRight.Y;

        public Node<T> FindNearestMarkUsed(Point p)
        {
            var best = (float.MaxValue, default(Node<T>));
            return FindNearestMarkUsed(p, ref best);
        }
        private Node<T> FindNearestMarkUsed(Point p, ref (float d, Node<T> node) best)
        {
            return FindNearestMarkUsed(p, ref best, this);
        }
        private Node<T> FindNearestMarkUsed(Point p, ref (float d, Node<T> node) best, QuadTree<T> quadTree)
        {
            var node = FindNearest(p, ref best,quadTree);
            if (node == null)
            {
                return null;
            }
            node.Used = true;
            return node;
        }
        public Node<T> FindNearest(Point p)
        {
            var best = (float.MaxValue, default(Node<T>));
            return FindNearest(p, ref best);
        }
        private Node<T> FindNearest(Point p, ref (float d, Node<T> node) best)
        {
            return FindNearest(p, ref best, this);
        }
        private Node<T> FindNearest(Point p, ref (float d, Node<T> node) best, QuadTree<T> quadTree)
        {
            if (p.X < quadTree.TopLeft.X - best.d ||
                p.X > quadTree.BotRight.X + best.d ||
                p.Y < quadTree.TopLeft.X - best.d ||
                p.Y > quadTree.BotRight.Y + best.d)
            {
                //Exclude node if point is farther away than best distance in either axis
                return best.node;
            }

            if (quadTree.Node != null 
                && quadTree.Node.Used == false) // hack until we can remove nodes from trees.
            {
                var dx = quadTree.Node.Point.X - p.X;
                var dy = quadTree.Node.Point.Y - p.Y;
                var delta = Mathf.Sqrt(dx * dx + dy * dy);
                if (delta < best.d)
                {
                    best.d = delta;
                    best.node = quadTree.Node;
                }
            }

            //We check if the kids is on the right or left, and then recurse the most likely candidates first.
            var right = (2 * p.X > quadTree.TopLeft.X + quadTree.BotRight.X) ? 1 : 0;
            var top = (2 * p.Y > quadTree.TopLeft.Y + quadTree.BotRight.Y) ? 1 : 0;

            var index0 = (1 - top) * 2 + right;         // if top & right  = (1 - 1) * 2 + 1        = 1
            var index1 = top * 2 + right;               // if top & right  = 1 * 2 + 1              = 3 // Hence on topright we will check, Topright,
            var index2 = (1 - top) * 2 + (1 - right);   // if top & right  = (1 - 1) * 2 + (1-1)    = 0
            var index3 = top * 2 + (1-right);           // if top  & right = 1*2 + (1-1)            = 2

            if (quadTree.Trees[index0] != null)
            {
                best.node = FindNearest(p, ref best, quadTree.Trees[index0]);
            }
            if (quadTree.Trees[index1] != null)
            {
                best.node = FindNearest(p, ref best, quadTree.Trees[index1]);
            }
            if (quadTree.Trees[index2] != null)
            {
                best.node = FindNearest(p, ref best, quadTree.Trees[index2]);
            }
            if (quadTree.Trees[index3] != null)
            {
                best.node = FindNearest(p, ref best, quadTree.Trees[index3]);
            }

            return best.node;
        }

        private QuadTree<T>[] Trees => new [] { TopLeftTree, TopRightTree, BotLeftTree, BotRightTree };

        public List<Node<T>> GetNodes()
        {
            var list = new List<Node<T>>();
            if (Node != null)
            {
                list.Add(Node);
            }
            if (TopLeftTree != null)
            {
                 list.AddRange(TopLeftTree.GetNodes());
            }

            if (BotLeftTree != null)
            {
                list.AddRange(BotLeftTree.GetNodes());
            }

            if (TopRightTree != null)
            {
                list.AddRange(TopRightTree.GetNodes());
            }

            if (BotRightTree != null)
            {
                list.AddRange(BotRightTree.GetNodes());
            }

            return list;
        }
    }
}
