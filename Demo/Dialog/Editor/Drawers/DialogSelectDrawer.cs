using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
using UnityEditor;

[CustomNodeGraphDrawer(typeof(DialogSelectNode))]
public class DialogSelectDrawer : NodeDrawer
{
    DialogSelectNode node;
    public override int Style { get { return 1; } }
    public override void OnInspectorGUI(NodeGUI gui)
    {
        base.OnInspectorGUI(gui);
        if (node == null)
        {
            node = target as DialogSelectNode;
            if (node.infomation == null)
            {
                node.infomation = new System.Collections.Generic.List<string>();
            }
        }
        if (GUILayout.Button("+", EditorStyles.toolbarDropDown))
        {
            node.infomation.Add("");
            gui.Data.AddOutputPoint(((char)(node.infomation.Count + 64)).ToString(),"");
        }
        for (int i = 0; i < node.infomation.Count; i++)
        {
            using (var hor = new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label(((char)(65 + i)).ToString(), GUILayout.Width(20));
                node.infomation[i] = EditorGUILayout.TextField(node.infomation[i]);
            }
        }
        if (GUILayout.Button("-", EditorStyles.toolbarDropDown))
        {
            if (node.infomation.Count > 0)
            {
                var id = node.infomation.Count - 1;
                gui.Data.OutputPoints.RemoveAt(id);
                node.infomation.RemoveAt(id);
            }
            return;
        }
    }
}
