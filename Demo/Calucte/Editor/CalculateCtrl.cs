using UnityEngine;
using System.Collections;

using NodeGraph;
using NodeGraph.DataModel;
using UnityEditor;
using NodeGraph.DefultSkin;
using System;

public class CalculateCtrl : NodeGraphController
{
    public override string Group { get { return "Calculate"; } }

    public override NodeGraphObj CreateNodeGraphObject(string path)
    {
        var obj = ScriptableObject.CreateInstance<NodeGraph.DataModel.NodeGraphObj>();
        obj.ControllerType = this.GetType().FullName;
        AssetDatabase.CreateAsset(obj, path);
        return obj;
    }

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
