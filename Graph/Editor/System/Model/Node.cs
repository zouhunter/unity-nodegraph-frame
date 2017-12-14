using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using Model=NodeGraph.DataModel;

namespace NodeGraph {
    /// <summary>
    /// Node.
    /// </summary>
	public abstract class Node {

		#region Node input output types

        /// <summary>
        /// Gets the valid type of the node input.
        /// </summary>
        /// <value>The type of the node input.</value>
		public virtual string NodeInputType {
			get {
				return null;
			}
		}

        /// <summary>
        /// Gets the valid type of the node output.
        /// </summary>
        /// <value>The type of the node output.</value>
		public virtual string NodeOutputType {
			get {
				return null;
			}
		}
		#endregion

        /// <summary>
        /// Category returns label string displayed at bottom of node.
        /// </summary>
        /// <value>The category.</value>
		public abstract string Category {
			get;
		}


		#region Initialization, Copy, Comparison, Validation
        /// <summary>
        /// Initialize Node with given NodeData.
        /// </summary>
        /// <param name="data">Data.</param>
		public abstract void Initialize(Model.NodeData data);

        /// <summary>
        /// Clone the node using newData.
        /// </summary>
        /// <param name="newData">New data.</param>
		public abstract Node Clone(Model.NodeData newData);

        /// <summary>
        /// Determines whether this instance is valid input connection point the specified point.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid input connection point the specified point; otherwise, <c>false</c>.</returns>
        /// <param name="point">Point.</param>
		public virtual bool IsValidInputConnectionPoint(Model.ConnectionPointData point) {
			return true;
		}
		#endregion

		#region GUI
        /// <summary>
        /// Gets the active style name in GUISkin.
        /// </summary>
        /// <value>The active style.</value>
		public abstract string ActiveStyle 	 { get; }

        /// <summary>
        /// Gets the inactive style name in GUISkin.
        /// </summary>
        /// <value>The inactive style.</value>
		public abstract string InactiveStyle { get; }

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
		public virtual void OnContextMenuGUI(GenericMenu menu) {
			// Do nothing
		}

		#endregion
	}

}
