using UnityEngine;
using UnityEditor;
using NodeGraph;
using System;

[CustomNodeGraphDrawer(typeof(BridgeConnection))]
public class BridgeConnectionDrawer : ConnectionDrawer
{
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
        var bridgeC = target as BridgeConnection;
        bridgeC.textString = EditorGUILayout.TextField(bridgeC.textString);
    }
}
