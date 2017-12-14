using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.Collections.Generic;
using NodeGraph.DataModel;
using NodeGraph;

[CustomNodeGraphDrawer(typeof(Luncher))]
public class LuncherDrawer : NodeDrawer
{
    public override string ActiveStyle
    {
        get
        {
            return "node 0 on";
        }
    }

    public override string InactiveStyle
    {
        get
        {
            return "node 0";
        }
    }

    public override string Category
    {
        get
        {
            return "empty";
        }
    }
    public override void Initialize(NodeData data)
    {
        data.AddDefaultOutputPoint();
        Debug.Log("Initialize");
    }
    public override void OnInspectorGUI(NodeGUI node, NodeGUIEditor editor, Action onValueChanged)
    {
        EditorGUILayout.HelpBox("Any Lunch: Lunch SubPanels From Any State", MessageType.Info);
        editor.UpdateNodeName(node);
    }
    public override void OnContextMenuGUI(GenericMenu menu)
    {
        base.OnContextMenuGUI(menu);
    }
}