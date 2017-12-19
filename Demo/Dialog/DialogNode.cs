using UnityEngine;
using System.Collections;

using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Dialog/Normal", 0, "Dialog")]
public class DialogNode : DialogEntryNode
{
    public override void Initialize(NodeData data)
    {
        base.Initialize(data);
        data.AddDefaultInputPoint();
    }
}
