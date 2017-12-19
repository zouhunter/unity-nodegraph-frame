using UnityEngine;
using System.Collections;

using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Node/Int", 2, "Calculate")]
public class IntNode : Node
{
    public override string NodeInputType
    {
        get
        {
            return "i";
        }
    }
    public override string NodeOutputType
    {
        get
        {
            return "i";
        }
    }
    public int value;
    public override void Initialize(NodeData data)
    {
        data.AddDefaultInputPoint();
        data.AddDefaultOutputPoint();
    }
}
