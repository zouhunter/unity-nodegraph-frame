using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
using System;

public class DialogCtrl : NodeGraphController
{
    public override string Group { get { return "Dialog"; } }
    public override NodeGraphObj CreateNodeGraphObject(string path)
    {
        var obj = ScriptableObject.CreateInstance<NodeGraph.DataModel.NodeGraphObj>();
        obj.ControllerType = this.GetType().FullName;
        UnityEditor.AssetDatabase.CreateAsset(obj, path);
        return obj;
    }
}
