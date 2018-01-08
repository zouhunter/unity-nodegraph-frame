using UnityEngine;
using System.Collections;

using NodeGraph;
using NodeGraph.DataModel;
using UnityEditor;

public class CalculateCtrl : NodeGraphController
{
    public override string Group { get { return "Calculate"; } }
    
    //internal override string GetConnectType(NodeData from, NodeData to)
    //{
    //    //var outType = from.Operation.Object.NodeOutputType;
    //    var inType = to.Operation.Object.NodeInputType;
    //    if(inType == NodePorts.stringData)
    //    {
    //        return "exprot";
    //    }
    //    else
    //    {
    //        return "calcute";
    //    }
    //}
}
