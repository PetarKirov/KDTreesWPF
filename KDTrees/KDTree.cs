using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDTrees
{ 
    public abstract class KDNode
    {
        public BoundingBox Boundaries { get; set; }

        public abstract KDNode[] ChildNodes { get; }

        public KDNode Left { get { return this.ChildNodes[0]; } }

        public KDNode Right { get { return this.ChildNodes[1]; } }
    }

    public class KDNodeWithChildNodes : KDNode
    {
        private KDNode[] childNodes = new KDNode[2];
        public override KDNode[] ChildNodes
        {
            get { return this.childNodes; }
        }

        public Axis SeparationAxis { get; set; }

        public double separationPoint { get; set; }
    }

    public class KDNodeWithGeometry : KDNode
    {
        public override KDNode[] ChildNodes
        {
            get { throw new InvalidOperationException("No child nodes!"); }
        }

        public readonly List<Triangle> geometry;

        public static KDNode CreateTree()
        {
            var root = new KDNodeWithGeometry()
            {
                //geometry = 
            };


            return root;
        }
    }
}
