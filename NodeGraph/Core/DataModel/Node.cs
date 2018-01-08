using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NodeGraph.DataModel
{
    /// <summary>
    /// Node.
    /// </summary>
	public abstract class Node{
        public virtual void Initialize(NodeData data)
        {
            if (inPoints != null && data.InputPoints.Count != inPoints.Count())
            {
                data.InputPoints.Clear();
                foreach (var point in inPoints)
                {
                    data.AddInputPoint(point.label, point.type, point.max);
                }
            }

            if(outPoints != null && data.OutputPoints.Count != outPoints.Count()) {
                data.OutputPoints.Clear();
                foreach (var point in outPoints)
                {
                    data.AddOutputPoint(point.label, point.type, point.max);
                }
            }
        }
        protected virtual IEnumerable<Point> inPoints { get { return null; } }
        protected virtual IEnumerable<Point> outPoints { get{ return null; } }
    }

}
