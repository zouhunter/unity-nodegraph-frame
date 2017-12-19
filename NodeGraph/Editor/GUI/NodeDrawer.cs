using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using NodeGraph.DataModel;
using UnityEngine.Events;
using System.Reflection;

namespace NodeGraph
{
    public class NodeDrawer: DefultDrawer
    {
        public Node target;
        public string ActiveStyle { get { return string.Format( "node {0} on",Style); } }
		public string InactiveStyle { get { return string.Format("node {0}",Style); } }
        public virtual int Style { get { return 0; } }
        public virtual string Category { get { return "empty"; } }
        public virtual void OnContextMenuGUI(GenericMenu menu,NodeGUI gui) { }
        public virtual float CustomNodeHeight { get { return 0; } }
        public virtual void OnNodeGUI(Rect position,NodeData data) { }
        public virtual void OnInspectorGUI(NodeGUI gui)
        {
            base.OnInspectorGUI(this.GetType().GetField("target"), target);
        }
        public virtual void OnClickNodeGUI(NodeGUI nodeGUI, Vector2 mousePosition, ConnectionPointData result) { }
        protected void RecordUnDo(string message,NodeGUI node,bool saveOnScopeEnd,UnityAction action)
        {
            using (new RecordUndoScope("Change Node Name", node, saveOnScopeEnd))
            {
                action.Invoke();
            }
        }
    }
}

