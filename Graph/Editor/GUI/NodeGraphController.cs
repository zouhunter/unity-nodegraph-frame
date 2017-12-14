using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;

using Model = NodeGraph.DataModel;
using BridgeUI.Model;
using BridgeUI;

namespace NodeGraph
{
    public class NodeGraphController
    {

        private List<NodeException> m_nodeExceptions;
        private Model.ConfigGraph m_targetGraph;

        public bool IsAnyIssueFound
        {
            get
            {
                return m_nodeExceptions.Count > 0;
            }
        }

        public List<NodeException> Issues
        {
            get
            {
                return m_nodeExceptions;
            }
        }

        public Model.ConfigGraph TargetGraph
        {
            get
            {
                return m_targetGraph;
            }
        }

        public NodeGraphController(Model.ConfigGraph graph)
        {
            m_targetGraph = graph;
            m_nodeExceptions = new List<NodeException>();
        }

        public void Perform()
        {
            LogUtility.Logger.Log(LogType.Log, "---Setup BEGIN---");

            foreach (var e in m_nodeExceptions)
            {
                var errorNode = m_targetGraph.Nodes.Find(n => n.Id == e.Id);
                // errorNode may not be found if user delete it on graph
                if (errorNode != null)
                {
                    LogUtility.Logger.LogFormat(LogType.Log, "[Perform] {0} is marked to revisit due to last error", errorNode.Name);
                    errorNode.NeedsRevisit = true;
                }
            }

            m_nodeExceptions.Clear();

            foreach (var item in TargetGraph.Nodes)
            {
                if (item.Operation.Object is IPanelInfoHolder)
                {
                    var nodeItem = item.Operation.Object as IPanelInfoHolder;
                    var guid = nodeItem.Info.prefabGuid;
                    if (string.IsNullOrEmpty(guid) || string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(guid)))
                    {
                        m_nodeExceptions.Add(new NodeException("prefab is null", item.Id));
                    }
                }
            }

            LogUtility.Logger.Log(LogType.Log, "---Setup END---");
        }

        internal void BuildToSelect()
        {
            LogUtility.Logger.Log(LogType.Log, "---On Build Clicked---");
        }
    }
}
