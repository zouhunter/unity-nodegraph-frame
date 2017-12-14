using UnityEditor;
using UnityEngine;
using NodeGraph;
using System;

[CustomConnection(KeyConst.BridgeName)]
public class BridgeConnection : Connection
{
    public string textString;
    internal override Color LineColor
    {
        get
        {
            return Color.red;
        }
    }
    internal override int LineWidth
    {
        get
        {
            return 8;
        }
    }
    internal override string Label
    {
        get
        {
            return "b";
        }
    }
    internal override void OnInspectorGUI(ConnectionGUI con, ConnectionGUIEditor connectionGUIEditor, Action onChanged)
    {
        textString = EditorGUILayout.TextField(textString);
    }
}
