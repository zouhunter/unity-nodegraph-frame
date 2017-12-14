using UnityEngine;
using System.Collections;

using NodeGraph;
using V1=NodeGraph.DataModel;

namespace NodeGraph.DataModel {
	public interface NodeDataImporter {
		void Import(V1.NodeData v1, NodeData v2);
	}
}
