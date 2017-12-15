using UnityEngine;
using UnityEditor;
using System;
using NodeGraph.DataModel;
using UnityEngine.Events;

namespace NodeGraph
{
    public abstract class NodeDrawer
    {
        public Node target;
        public abstract string ActiveStyle { get; }
		public abstract string InactiveStyle { get; }
        public abstract string Category { get; }
        public abstract void OnInspectorGUI(NodeData data,Action onValueChanged);
        public virtual void OnContextMenuGUI(GenericMenu menu)
        {
            // Do nothing
        }
       
    }
}

