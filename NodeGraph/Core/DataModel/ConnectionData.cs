using UnityEngine;
using System;
using System.Collections.Generic;
using NodeGraph;

namespace NodeGraph.DataModel {
    /// <summary>
    /// Connection data.
    /// </summary>
	[Serializable]
	public class ConnectionData
    {
        [SerializeField] private string m_id;
		[SerializeField] private string m_fromNodeId;
		[SerializeField] private string m_fromNodeConnectionPointId;
		[SerializeField] private string m_toNodeId;
		[SerializeField] private string m_toNodeConnectionPoiontId;
		[SerializeField] private string m_type;
        [SerializeField] private Connection m_connection;

        #region Propertys
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id
        {
            get
            {
                return m_id;
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
		public string ConnectionType
        {
            get
            {
                return m_type;
            }

            set
            {
                m_type = value;
            }
        }

        /// <summary>
        /// Gets from node identifier.
        /// </summary>
        /// <value>From node identifier.</value>
		public string FromNodeId
        {
            get
            {
                return m_fromNodeId;
            }
        }

        /// <summary>
        /// Instence Of Connection
        /// </summary>
        public Connection Object
        {
            get
            {
                return m_connection;
            }
            set
            {
                m_connection = value;
            }
        }

        /// <summary>
        /// Gets from node connection point identifier.
        /// </summary>
        /// <value>From node connection point identifier.</value>
        public string FromNodeConnectionPointId
        {
            get
            {
                return m_fromNodeConnectionPointId;
            }
        }

        /// <summary>
        /// Gets to node identifier.
        /// </summary>
        /// <value>To node identifier.</value>
		public string ToNodeId
        {
            get
            {
                return m_toNodeId;
            }
        }

        /// <summary>
        /// Gets to node connection point identifier.
        /// </summary>
        /// <value>To node connection point identifier.</value>
		public string ToNodeConnectionPointId
        {
            get
            {
                return m_toNodeConnectionPoiontId;
            }
        }
        #endregion

        public ConnectionData(string type, Connection connection, ConnectionPointData output, ConnectionPointData input) {
            m_id = Guid.NewGuid().ToString();
            m_type = type;
            m_fromNodeId = output.NodeId;
			m_fromNodeConnectionPointId = output.Id;
			m_toNodeId = input.NodeId;
			m_toNodeConnectionPoiontId = input.Id;
            m_connection = connection;
            m_connection.name = connection.name;
        }

        public bool Validate()
        {
            if (m_connection == null){
                //Debug.Log("Revert:" + Id);
                m_connection = ScriptObjectCatchUtil.Revert(Id) as Connection;
            }
            return m_connection != null;
        }


		public bool Validate (List<NodeData> allNodes, List<ConnectionData> allConnections) {

			var fromNode = allNodes.Find(n => n.Id == this.FromNodeId);
			var toNode   = allNodes.Find(n => n.Id == this.ToNodeId);

			if(fromNode == null) {
				return false;
			}

			if(toNode == null) {
				return false;
			}

			var outputPoint = fromNode.FindOutputPoint(this.FromNodeConnectionPointId);
			var inputPoint  = toNode.FindInputPoint(this.ToNodeConnectionPointId);

			if(null == outputPoint) {
				return false;
			}

			if(null == inputPoint) {
				return false;
			}

			// update connection label if not matching with outputPoint label
			if( outputPoint.Type != m_type ) {
				m_type = outputPoint.Type;
			}

			return true;
		}
   
	}
}
