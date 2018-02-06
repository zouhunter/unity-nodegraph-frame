using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
using System.Collections.Generic;

[CustomNode("Node/Float",1,"Calculate")]
public class FloatNode : Node
{
    public float value;
    protected override IEnumerable<Point> inPoints
    {
        get
        {
            return new Point[] { new Point("f", "Number", 2) };
        }
    }
    protected override IEnumerable<Point> outPoints
    {
        get
        {
            return new Point[] { new Point("f", "Number", 2) };
        }
    }
}
