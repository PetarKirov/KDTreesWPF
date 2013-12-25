using KDTrees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPlayground
{
    public class DrawingCanvasViewModel
    {
        public List<Triangle> Geometry { get; private set; }

        public KDNode KDTree { get; private set; }

        public List<BoundingBox> Boxes { get; private set; }

        public DrawingCanvasViewModel()
        {
            this.Geometry = new List<Triangle>();
            this.Boxes = new List<BoundingBox>();
        }

        public void AddTriangle(Triangle t)
        {
            this.Geometry.Add(t);
        }

        public void BuildTree()
        {
            this.KDTree = KDNode.BuildTree(this.Geometry);
        }

        public void Reset()
        {
            this.Geometry.Clear();
            this.Boxes.Clear();
            this.KDTree = null;
        }
    }
}
