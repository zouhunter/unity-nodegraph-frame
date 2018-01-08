using UnityEngine;
using System.Collections;

using NodeGraph;
using System.Collections.Generic;
using NodeGraph.DataModel;
[CustomNode("Dialog/Normal", 0, "Dialog")]
public class DialogNode : DialogEntryNode
{
    protected override IEnumerable<Point> inPoints
    {
        get
        {
            return new Point[] { new Point("->", "", 1) };
        }
    }
    protected override IEnumerable<Point> outPoints
    {
        get
        {
            return new Point[] { new Point("->", "", 1) };
        }
    }
}
