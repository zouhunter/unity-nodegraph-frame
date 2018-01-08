using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Result", 10, "Calculate")]
public class ResultNode : Node
{
    public string value;
}
