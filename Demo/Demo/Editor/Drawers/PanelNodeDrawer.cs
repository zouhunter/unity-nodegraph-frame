﻿using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.Collections.Generic;
using NodeGraph.DataModel;
using NodeGraph;

[CustomNodeGraphDrawer(typeof(PanelNode))]
public class PanelNodeDrawer : NodeDrawer
{
    public override string ActiveStyle
    {
        get
        {
            return "node 1 on";
        }
    }

    public override string InactiveStyle
    {
        get
        {
            return "node 1";
        }
    }

    public override string Category
    {
        get
        {
            return "panel";
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Any Lunch: Lunch SubPanels From Any State", MessageType.Info);
        (target as PanelNode).key =  EditorGUILayout.TextField((target as PanelNode).key);
    }
    public override void OnContextMenuGUI(GenericMenu menu)
    {
        base.OnContextMenuGUI(menu);
    }
}