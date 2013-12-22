using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTrees
{ 
    public class KDNode
    {
        public KDNode(BoundingBox boundaries, Axis separationAxis, double separationPoint = double.NaN)
        {
            if (separationAxis == Axis.None)
                this.Geometry = new List<Triangle>();
            else
                this.ChildNodes = new KDNode[2];

            this.SeparationAxis = separationAxis;
            this.SeparationPoint = SeparationPoint;
        }

        public BoundingBox Boundaries { get; private set; }

        public KDNode[] ChildNodes { get; private set; }

        public readonly List<Triangle> Geometry { get; private set; }

        public Axis SeparationAxis { get; private set; }

        public double SeparationPoint { get; private set; }
    }
}
