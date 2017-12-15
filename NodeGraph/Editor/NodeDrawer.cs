using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using NodeGraph.DataModel;
using UnityEngine.Events;
using System.Reflection;

namespace NodeGraph
{
    public class NodeDrawer
    {
        public Node target;
        protected SerializedObject serializedObject;
        private List<FieldInfo> fields;
        public virtual string ActiveStyle { get { return "node 0 on"; } }
		public virtual string InactiveStyle { get { return "node 0"; } }
        public virtual string Category { get { return "empty"; } }
        public virtual void OnInspectorGUI(NodeData data,Action onValueChanged)
        {
            var node = data.Operation.Object;
            if(fields == null)
            {
                fields = new List<FieldInfo>();
                UserDefineUtility.GetNeedSerializeField(node, fields);
            }
            using (var ver = new EditorGUILayout.VerticalScope())
            {
                foreach (var item in fields)
                {
                    using (var hor = new EditorGUILayout.HorizontalScope())
                    {
                        EditorGUILayout.LabelField(item.Name);
                        item.SetValue(node, UserDefineUtility.DrawProperty(item.GetValue(node)));
                    }
                }
            }
         
        }
        public virtual void OnContextMenuGUI(GenericMenu menu) { }
       
    }
}

