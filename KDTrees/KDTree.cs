using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTrees.Utility;
using System.Threading;

namespace KDTrees
{
    public class MessageDelegator
    {
        private MessageDelegator() { }

        public static readonly MessageDelegator Instance = new MessageDelegator();

        public event KDTrees.KDNode.KDNodeCreatedEventHandler KDNodeCreated;

        internal void RaiseNodeCreated(KDNode nodeSender, Pair<BoundingBox> splittedBox)
        {
            if (this.KDNodeCreated != null)
                this.KDNodeCreated(this, new KDTrees.KDNode.KDNodeCreatedEventArgs(splittedBox));
        }
    }

    public class KDNode
    {
        public class KDNodeCreatedEventArgs : EventArgs
        {
            public Pair<BoundingBox> SplittedBox { get; private set; }

            public KDNodeCreatedEventArgs(Pair<BoundingBox> splittedBox)
            {
                this.SplittedBox = splittedBox;
            }
        }

        public delegate void KDNodeCreatedEventHandler(object sender, KDNodeCreatedEventArgs args);

        public KDNode() { }

        public KDNode(BoundingBox boundaries, List<IGeometry> geometry = null, Axis separationAxis = Axis.None, double separationPoint = double.NaN)
        {
            if (separationAxis == Axis.None && geometry != null)
                this.Geometry = geometry;
            else
                this.ChildNodes = new KDNode[2];

            this.Boundaries = boundaries;
            this.SeparationAxis = separationAxis;
            this.SeparationPoint = SeparationPoint;
        }

        public BoundingBox Boundaries { get; private set; }

        public KDNode[] ChildNodes { get; private set; }

        public KDNode Left
        {
            get { return this.ChildNodes[0]; }
            set { this.ChildNodes[0] = value; }
        }

        public KDNode Right
        {
            get { return this.ChildNodes[1]; }
            set { this.ChildNodes[1] = value; }
        }

        public List<IGeometry> Geometry { get; private set; }

        public Axis SeparationAxis { get; private set; }

        public double SeparationPoint { get; private set; }

        public static KDNode BuildTree(List<IGeometry> geometry, BoundingBox area)
        {
            KDNode root = new KDNode(area, geometry.ToList());

            BuildTree(root, 1, Axis.X);

            return root;
        }

        private const int MaxDepth = 20;
        private const int MinTrianglesPerLeaf = 2; //should be more

        private static void BuildTree(KDNode node, uint depth, Axis axis)
        {
            if (depth > MaxDepth || node.Geometry.Count < MinTrianglesPerLeaf)
                return;

            node.SeparationPoint = 0.5;
            var boxes = node.Boundaries.SplitInHalf(axis);

            MessageDelegator.Instance.RaiseNodeCreated(node, boxes);
            Thread.Sleep(1000);

            var leftGeometry = node.Geometry.TakeIf(triangle => triangle.IsContainedIn(boxes.A));
            var rightGeometry = node.Geometry.TakeAll();

            node.ChildNodes = new KDNode[2];
            node.Left = new KDNode(boxes.A, leftGeometry);
            node.Right = new KDNode(boxes.B, rightGeometry);

            BuildTree(node.Left, depth + 1, axis.Next2D());
            BuildTree(node.Right, depth + 1, axis.Next2D());

            return;
        }
    }
}
