using UnityEngine;
using UnityEngine.Events;
using Model = NodeGraph.DataModel;
using NodeGraph;
using System;
using UnityEditor;
using BridgeUI.Model;
using BridgeUI;

public abstract class PanelNodeBase : Node, IPanelInfoHolder
{
    protected const int lableWidth = 120;
    protected GameObject prefab;
    public NodeInfo nodeInfo = new NodeInfo();
    public NodeInfo Info
    {
        get
        {
            return nodeInfo;
        }
    }
    protected abstract string HeadInfo { get; }
    public override Node Clone(Model.NodeData newData)
    {
        return Clone(newData);
    }

    public override void OnInspectorGUI(NodeGUI node, NodeGUIEditor editor, Action onValueChanged)
    {
        EditorGUILayout.HelpBox(HeadInfo, MessageType.Info);
        editor.UpdateNodeName(node);
        LoadRecordIfEmpty();
        DrawNodeInfo(node, onValueChanged);
        if(prefab != null) node.Name = prefab.name;
    }

    protected virtual void DrawNodeInfo(NodeGUI node, Action onValueChanged)
    {
        DrawHeadSelect();

        EditorGUILayout.HelpBox("[窗体信息配制:]", MessageType.Info);
        
        if (ChangeCheckField(DrawHeadField))
        {
            RecordPrefabInfo(node);
            onValueChanged.Invoke();
        }

        if (ChangeCheckField(DrawInforamtion))
        {
            onValueChanged.Invoke();
        }
    }

    protected abstract void DrawHeadSelect();

    protected virtual void LoadRecordIfEmpty()
    {
        if (prefab == null && !string.IsNullOrEmpty(nodeInfo.prefabGuid))
        {
            var path = AssetDatabase.GUIDToAssetPath(nodeInfo.prefabGuid);
            if (!string.IsNullOrEmpty(path))
            {
                prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            }
            else
            {
                nodeInfo.prefabGuid = null;
            }
        }
    }

    private void RecordPrefabInfo(NodeGUI node)
    {
        if (prefab != null)
        {
            var path = AssetDatabase.GetAssetPath(prefab);
            nodeInfo.prefabGuid = AssetDatabase.AssetPathToGUID(path);
        }
    }
    protected abstract void DrawHeadField();
    protected abstract void DrawInforamtion();
    protected void DrawObjectFieldInternal()
    {
        using (var hor = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("【预制体】:", EditorStyles.largeLabel, GUILayout.Width(lableWidth));
            prefab = EditorGUILayout.ObjectField(prefab, typeof(GameObject), false) as GameObject;
        }
    }
  
    private bool ChangeCheckField(UnityAction func)
    {
        EditorGUI.BeginChangeCheck();
        func.Invoke();
        return EditorGUI.EndChangeCheck();

    }
}