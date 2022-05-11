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
            return topLeft.X <= X && X <= bottomRight.X && topLeft.Y <= Y && Y <= bottomRight.Y;
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

            if (Node != null && Node.Point.LiesWithin(topLeft, botRight))
            {
                result.Add(Node);
                return result;
            }
            var outOfBounds = !(topLeft.LiesWithin(TopLeft, BotRight) || botRight.LiesWithin(TopLeft, BotRight));
            if (outOfBounds) // We are only out of bounds if both points are.. Right?!
            {
                return result;
            }
            for (int i = 0; i < trees.Length; i++)
            {
                if (trees[i] != null)
                {
                    result.AddRange(trees[i].FindInQuadrant(topLeft, botRight));
                }
            }
            return result;
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
            return PopNearest(p, best, this);
        }
        private (float d, Node<T> node) PopNearest(Point p, (float d, Node<T> node) best, QuadTree<T> quadTree)
        {
            var result = FindNearest(p, best,quadTree);
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

        public (float d, Node<T> node) FindNearest(Point p)
        {
            var best = (float.MaxValue, default(Node<T>));
            return FindNearest(p, best, this);
        }
        private (float d, Node<T> node) FindNearest(Point p, (float d, Node<T> node) best, QuadTree<T> quadTree)
        {
            if (p.X < quadTree.TopLeft.X - best.d ||
                p.X > quadTree.BotRight.X + best.d ||
                p.Y < quadTree.BotRight.Y - best.d ||
                p.Y > quadTree.TopLeft.Y + best.d)
            {
                //Exclude node if point is farther away than best distance in either axis
                return best;
            }

            if (quadTree.Node != null)
            {
                var delta = p.DistanceTo(quadTree.Node.Point);
                if (delta < best.d)
                {
                    best.d = delta;
                    best.node = quadTree.Node;
                    best.node.Tree = quadTree;
                }
            }

            //We check if the kids is on the right or left, and then recurse the most likely candidates first.
            var right = (2 * p.X > quadTree.TopLeft.X + quadTree.BotRight.X) ? 1 : 0;
            var top = (2 * p.Y > quadTree.TopLeft.Y + quadTree.BotRight.Y) ? 1 : 0;

            var index0 = (1 - top) * 2 + right;         // if top & right  = (1 - 1) * 2 + 1        = 1
            var index1 = top * 2 + right;               // if top & right  = 1 * 2 + 1              = 3 // Hence on topright we will check, Topright,
            var index2 = (1 - top) * 2 + (1 - right);   // if top & right  = (1 - 1) * 2 + (1-1)    = 0
            var index3 = top * 2 + (1-right);           // if top  & right = 1*2 + (1-1)            = 2

            if (quadTree.trees[index0] != null)
            {
                best = FindNearest(p, best, quadTree.trees[index0]);
            }
            if (quadTree.trees[index1] != null)
            {
                best = FindNearest(p, best, quadTree.trees[index1]);
            }
            if (quadTree.trees[index2] != null)
            {
                best = FindNearest(p, best, quadTree.trees[index2]);
            }
            if (quadTree.trees[index3] != null)
            {
                best = FindNearest(p, best, quadTree.trees[index3]);
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
