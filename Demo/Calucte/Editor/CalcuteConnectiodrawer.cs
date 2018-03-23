using UnityEngine;
using System.Collections;
using UnityEditor;
using NodeGraph;
using NodeGraph.DataModel;

[CustomNodeView(typeof(OperationConnection))]
public class CalcuteConnectiodrawer : ConnectionView
{
    internal override Color LineColor {
        get { return Color.green; }
    }
    internal override int LineWidth { get { return 1; } }

    protected string Label { get { return (target as OperationConnection).calcuteType.ToString().Substring(0,1); } }
}
