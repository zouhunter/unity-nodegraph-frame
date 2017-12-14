using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using NodeGraph.DataModel;

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


		#region Initialization, Copy, Comparison, Validation
        /// <summary>
        /// Clone the node using newData.
        /// </summary>
        /// <param name="newData">New data.</param>
		public abstract Node Clone(NodeData newData);
        /// <summary>
        /// Determines whether this instance is valid input connection point the specified point.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid input connection point the specified point; otherwise, <c>false</c>.</returns>
        /// <param name="point">Point.</param>
        public virtual bool IsValidInputConnectionPoint(ConnectionPointData point)
        {
            return true;
        }
        #endregion
    }

}
