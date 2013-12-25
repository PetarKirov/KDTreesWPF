using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTrees.Utility;

namespace KDTrees
{ 
    public class KDNode
    {
        public KDNode() { }

        public KDNode(BoundingBox boundaries, List<Triangle> geometry = null,  Axis separationAxis = Axis.None, double separationPoint = double.NaN)
        {
            if (separationAxis == Axis.None && geometry != null)
                this.Geometry = geometry;
            else
                this.ChildNodes = new KDNode[2];

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

        public List<Triangle> Geometry { get; private set; }

        public Axis SeparationAxis { get; private set; }

        public double SeparationPoint { get; private set; }

        public static KDNode BuildTree(List<Triangle> geometry)
        {
            KDNode root = new KDNode(BoundingBox.MaxValue, geometry);

            BuildTree(root, 1);

            return root;
        }

        private const int MaxDepth = 20;
        private const int MinTrianglesPerLeaf = 20;
        
        private static void BuildTree(KDNode node, uint depth)
        {
            if (depth > MaxDepth || node.Geometry.Count < MinTrianglesPerLeaf)
                return;

            node.SeparationPoint = 0.5;
            var boxes = node.Boundaries.SplitInHalf(node.SeparationAxis);

            var leftGeometry = node.Geometry.TakeIf(triangle => boxes.A.Contains(triangle));
            var rightGeometry = node.Geometry.TakeAll();

            node.Left = new KDNode(boxes.A, leftGeometry);
            node.Right = new KDNode(boxes.B, rightGeometry);

            BuildTree(node.Left, depth + 1);
            BuildTree(node.Right, depth + 1);

            return;
        }
    }
}
