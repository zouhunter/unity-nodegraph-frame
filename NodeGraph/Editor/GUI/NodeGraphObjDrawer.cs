using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using NodeGraph.DataModel;

namespace NodeGraph {

	[CustomEditor(typeof(NodeGraphObj))]
	public class NodeGraphObjDrawer : Editor {
		//private class Styles {
		//	public static readonly string kEDITBUTTON_LABEL		= "Open in Graph Editor";
		//	public static readonly string kEDITBUTTON_DESCRIPTION	= "Opens in the Node Graph Editor, which will allow you to configure the graph";
		//	public static readonly GUIContent kEDITBUTTON = new GUIContent(kEDITBUTTON_LABEL, kEDITBUTTON_DESCRIPTION);
		//}

		//public override void OnInspectorGUI()
		//{
		//	Model.NodeGroup graph = target as Model.NodeGroup;
            
  //          using (new EditorGUILayout.HorizontalScope())
  //          {
  //              GUILayout.Label("NodeGraphController", "BoldLabel");
  //              GUILayout.Label(graph.ControllerType);
  //          }

  //          using (new EditorGUILayout.HorizontalScope()) {
		//		GUILayout.Label(graph.name, "BoldLabel");
		//		if (GUILayout.Button(Styles.kEDITBUTTON, GUILayout.Width(150f), GUILayout.ExpandWidth(false)))
		//		{
		//			// Get the target we are inspecting and open the graph
		//			var window = EditorWindow.GetWindow<NodeGraphWindow>();
		//			window.OpenGraph(graph);
		//		}
		//	}

		//	using(new EditorGUILayout.VerticalScope(GUI.skin.box)) {
		//		//EditorGUILayout.LabelField("Version", graph.Version.ToString());
		//		EditorGUILayout.LabelField("Last Modified", graph.LastModified.ToString());
		//		using(new EditorGUILayout.HorizontalScope()) {
		//			GUILayout.Label("Description", GUILayout.Width(100f));
		//			string newdesc = EditorGUILayout.TextArea(graph.Descrption, GUILayout.MaxHeight(100f));
		//			if(newdesc != graph.Descrption) {
		//				graph.Descrption = newdesc;
		//			}
		//		}
		//		GUILayout.Space(2f);
		//	}
		//}

        public static GUIStyle titleStyle;
        public static GUIStyle subTitleStyle;
        public static GUIStyle boldLabelStyle;

        public NodeGraphObj canvas;

        public void OnEnable()
        {
            canvas = (NodeGraphObj)target;
            //canvas.Validate();
        }

        public override void OnInspectorGUI()
        {
            if (canvas == null)
                canvas = (NodeGraphObj)target;
            if (canvas == null)
                return;
            if (titleStyle == null)
            {
                titleStyle = new GUIStyle(GUI.skin.label);
                titleStyle.fontStyle = FontStyle.Bold;
                titleStyle.alignment = TextAnchor.MiddleCenter;
                titleStyle.fontSize = 16;
            }
            if (subTitleStyle == null)
            {
                subTitleStyle = new GUIStyle(GUI.skin.label);
                subTitleStyle.fontStyle = FontStyle.Bold;
                subTitleStyle.alignment = TextAnchor.MiddleCenter;
                subTitleStyle.fontSize = 12;
            }
            if (boldLabelStyle == null)
            {
                boldLabelStyle = new GUIStyle(GUI.skin.label);
                boldLabelStyle.fontStyle = FontStyle.Bold;
            }

            EditorGUI.BeginChangeCheck();

            GUILayout.Space(10);

            GUILayout.Label(new GUIContent(canvas.ControllerType, "自己定义控制器类型"), titleStyle);
            GUILayout.Label(canvas.LastModified.ToString("yyyy-MM-dd hh:mm:ss"), subTitleStyle);
            //GUILayout.Label("Type: " + canvas.Descrption, subTitleStyle);

            GUILayout.Space(10);

            if (GUILayout.Button("Open",EditorStyles.toolbarButton))
            {
                var window = EditorWindow.GetWindow<NodeGraphWindow>();
                window.OpenGraph(canvas);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label("Description", GUILayout.Width(100f));
                string newdesc = EditorGUILayout.TextArea(canvas.Descrption, GUILayout.MaxHeight(100f));
                if (newdesc != canvas.Descrption)
                {
                    canvas.Descrption = newdesc;
                }
            }

          

            GUILayout.Space(10);

            if(canvas.Nodes.Count > 0)
            {
                GUILayout.Label("[Nodes]", boldLabelStyle);

                foreach (NodeData node in canvas.Nodes)
                {
                    string label = node.Name;
                    EditorGUILayout.ObjectField(label, node.Object, node.Object.GetType(), false);
                }

                GUILayout.Space(10);

            }

            if(canvas.Connections.Count > 0)
            {
                GUILayout.Label("[Connections]", boldLabelStyle);

                foreach (var connection in canvas.Connections)
                {
                    string label = connection.ConnectionType;
                    EditorGUILayout.ObjectField(label, connection.Object, connection.Object.GetType(), false);
                }

                GUILayout.Space(10);
            }
        

            if (EditorGUI.EndChangeCheck())
            {
                
            }
        }
    }
}
	