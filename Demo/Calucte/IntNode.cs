using UnityEngine;
using System.Collections;

using NodeGraph;
using NodeGraph.DataModel;
using System.Collections.Generic;


[CustomNode("Node/Short", 2, "Calculate")]
public class ShortNode : Node
{
    public short value;
}


[CustomNode("Node/Int", 2, "Calculate")]
public class IntNode : Node
{
    public int value;
    protected override IEnumerable<Point> inPoints
    {
        get
        {
            return new Point[] { new Point("i", "Number", 2) };
        }
    }
    protected override IEnumerable<Point> outPoints
    {
        get
        {
            return new Point[] { new Point("i", "Number", 2) };
        }
    }
}
