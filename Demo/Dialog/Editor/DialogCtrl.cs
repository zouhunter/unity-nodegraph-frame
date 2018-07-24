using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
using System;

public class DialogCtrl : NodeGraphController<NodeGraph.DataModel.NodeGraphObj>
{
    public override string Group { get { return "Dialog"; } }
}
