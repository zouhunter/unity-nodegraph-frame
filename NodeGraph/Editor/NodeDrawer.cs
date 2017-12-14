using UnityEngine;
using UnityEditor;
using System;
using NodeGraph.DataModel;

namespace NodeGraph
{
    public abstract class NodeDrawer
    {
        public Node target;
        
        #region GUI
        /// <summary>
        /// Gets the active style name in GUISkin.
        /// </summary>
        /// <value>The active style.</value>
        public abstract string ActiveStyle { get; }

        /// <summary>
        /// Gets the inactive style name in GUISkin.
        /// </summary>
        /// <value>The inactive style.</value>
		public abstract string InactiveStyle { get; }


        /// <summary>
        /// Category returns label string displayed at bottom of node.
        /// </summary>
        /// <value>The category.</value>
        public abstract string Category
        {
            get;
        }

        #endregion
        public abstract void Initialize(NodeData data);
        /// <summary>
        /// Raises the inspector GU event.
        /// </summary>
        /// <param name="node">NodeGUI instance for this node.</param>
        /// <param name="streamManager">Manager instance to retrieve graph's incoming/outgoing group of assets.</param>
        /// <param name="editor">Helper instance to draw inspector.</param>
        /// <param name="onValueChanged">Action to call when OnInspectorGUI() changed value of this node.</param>
        public abstract void OnInspectorGUI(NodeGUI node, NodeGUIEditor editor, Action onValueChanged);

        /// <summary>
        /// OnContextMenuGUI() is called when Node is clicked for context menu.
        /// </summary>
        /// <param name="menu">Context menu instance.</param>
        public virtual void OnContextMenuGUI(GenericMenu menu)
        {
            // Do nothing
        }
       
    }
}

