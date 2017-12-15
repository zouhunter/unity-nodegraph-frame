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
		public virtual string NodeInputType {
			get {
				return null;
			}
		}
		public virtual string NodeOutputType {
			get {
				return null;
			}
		}
        public abstract void Initialize(NodeData data);
        public virtual bool IsValidInputConnectionPoint(ConnectionPointData point) { return true; }
    }

}
