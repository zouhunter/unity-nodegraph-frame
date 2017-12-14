using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using V1 = NodeGraph.DataModel;
using Model = NodeGraph.DataModel;
using NodeGraph;
using System;
using UnityEditor;


[CustomNode("Panel", 1)]
public class PanelNode : PanelNodeBase
{
    public NodeType nodeType = NodeType.Destroy | NodeType.Fixed | NodeType.HideGO | NodeType.NoAnim | NodeType.ZeroLayer;
    public int style;
    public override string ActiveStyle
    {
        get
        {
            return string.Format("node {0} on", style);
        }
    }

    public override string InactiveStyle
    {
        get
        {
            return string.Format("node {0}", style);
        }
    }

    public override string Category
    {
        get
        {
            return "panel";
        }
    }

    protected override string HeadInfo
    {
        get
        {
            return "Panel Node : record panel load type and other rule";
        }
    }
    public PanelNode()
    {

    }
    public PanelNode(string prefabPath)
    {
        Info.prefabGuid = AssetDatabase.AssetPathToGUID(prefabPath);
    }
    public override void Initialize(Model.NodeData data)
    {
        data.AddDefaultOutputPoint();
        data.AddDefaultInputPoint();
    }

    public override void OnContextMenuGUI(GenericMenu menu)
    {
        base.OnContextMenuGUI(menu);
    }

    protected override void DrawInforamtion()
    {
       
    }
    private void DrawToggleFromNodeType(NodeType model)
    {
        var on = (nodeType & model) == model;
        using (var hor = new EditorGUILayout.HorizontalScope())
        {
            var thison = GUILayout.Toggle(on, model.ToString(), EditorStyles.radioButton, GUILayout.Width(60));

            if (thison != on)
            {
                on = thison;
                if (on)
                {
                    nodeType |= model;
                }
                else
                {
                    nodeType &= ~model;
                }
            }
        }
    }
    protected override void DrawHeadSelect()
    {
        using (var hor = new EditorGUILayout.HorizontalScope())
        {
            DrawToggleFromNodeType(NodeType.Fixed);
            DrawToggleFromNodeType(NodeType.ZeroLayer);
            DrawToggleFromNodeType(NodeType.HideGO);
            DrawToggleFromNodeType(NodeType.Destroy);
            DrawToggleFromNodeType(NodeType.NoAnim);
        }
        using (var hor = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Style:");
            style = (int)EditorGUILayout.Slider(style, 1, 7);
        }
    }
    protected override void DrawHeadField()
    {
        if (nodeType != 0)
        {
            DrawObjectFieldInternal();
        }
    }
}
