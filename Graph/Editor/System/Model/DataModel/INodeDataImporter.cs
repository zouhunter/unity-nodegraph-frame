using UnityEngine;
using System.Collections;

using NodeGraph;
using V1=NodeGraph.DataModel;

namespace NodeGraph.DataModel {
	public interface INodeDataImporter {
		void Import(V1.NodeData v1, NodeData v2);
	}
}
