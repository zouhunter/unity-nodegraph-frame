using UnityEngine;
using System.Collections;
using UnityEditor;
using NodeGraph;
using NodeGraph.DataModel;
[CustomNodeGraphDrawer(typeof(FloatNode),typeof(IntNode))]
public class FloatNodeDrawer : NodeDrawer {
    public override float CustomNodeHeight { get { return EditorGUIUtility.singleLineHeight * 2; } }
    IntNode iNode;
    FloatNode fNode;

    public override void OnNodeGUI(Rect position, NodeData data)
    {
        base.OnNodeGUI(position, data);
        position = new Rect(12, position.y + 10, position.width - 24, EditorGUIUtility.singleLineHeight);
        if (data.Object is IntNode)
        {
            iNode = data.Object as IntNode;
            iNode.value = EditorGUI.IntField(position, iNode.value);
        }
        if(data.Object is FloatNode)
        {
            fNode = data.Object as FloatNode;
            fNode.value = EditorGUI.FloatField(position, fNode.value);
        }

    }
}
