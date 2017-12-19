using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Node/Float",1,"Calculate")]
public class FloatNode : Node
{
    public override string NodeInputType
    {
        get
        {
            return "f";
        }
    }
    public override string NodeOutputType
    {
        get
        {
            return "f";
        }
    }
    public float value;
    public override void Initialize(NodeData data)
    {
        data.AddDefaultInputPoint();
        data.AddDefaultOutputPoint();
    }
}
