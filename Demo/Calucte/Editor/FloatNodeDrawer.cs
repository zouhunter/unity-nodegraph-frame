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
        if (data.Operation.Object is IntNode)
        {
            iNode = data.Operation.Object as IntNode;
            iNode.value = EditorGUI.IntField(position, iNode.value);
        }
        if(data.Operation.Object is FloatNode)
        {
            fNode = data.Operation.Object as FloatNode;
            fNode.value = EditorGUI.FloatField(position, fNode.value);
        }

    }
}
