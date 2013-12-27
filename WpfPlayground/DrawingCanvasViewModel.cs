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
        public List<IGeometry> Geometry { get; private set; }

        public KDNode KDTree { get; private set; }

        public List<BoundingBox> Boxes { get; private set; }

        public DrawingCanvasViewModel()
        {
            this.Geometry = new List<IGeometry>();
            this.Boxes = new List<BoundingBox>();
        }

        public void AddGeometry(IGeometry geomElement)
        {
            this.Geometry.Add(geomElement);
        }

        public async Task BuildTree(double width, double height)
        {
            await Task.Run(() =>
                {
                    this.KDTree = KDNode.BuildTree(this.Geometry, new BoundingBox(width, height));
                });
        }

        public void Reset(bool onlyTree)
        {
            if (!onlyTree)
            {
                this.Geometry.Clear();
                this.Boxes.Clear();
            }
            this.KDTree = null;
        }
    }
}
