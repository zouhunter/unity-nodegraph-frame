using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.Collections.Generic;
using NodeGraph.DataModel;

namespace NodeGraph
{
    [Serializable]
    public class ConnectionGUI
    {
        [SerializeField]
        private ConnectionData m_data;

        [SerializeField]
        private ConnectionPointData m_outputPoint;
        [SerializeField]
        private ConnectionPointData m_inputPoint;

        private ConnectionGUIInspectorHelper m_inspector;
        private NodeGraphController m_controller;
        private ConnectionView _connectionDrawer;
        private ConnectionView connectionDrawer
        {
            get
            {
                if(_connectionDrawer == null)
                {
                    if(m_data == null || m_data.Object == null)
                    {
                        _connectionDrawer = new ConnectionView();
                    }
                    else
                    {
                        _connectionDrawer = UserDefineUtility.GetUserDrawer(m_data.Object.GetType()) as ConnectionView;
                        if (_connectionDrawer == null) _connectionDrawer = new ConnectionView();
                        _connectionDrawer.target = m_data.Object;
                    }
                }
                return _connectionDrawer;
            }
        }
        public string ConnectionType
        {
            get
            {
                return m_data.ConnectionType;
            }
            set
            {
                m_data.ConnectionType = value;
            }
        }

        public string Id
        {
            get
            {
                return m_data.Id;
            }
        }

        public ConnectionData Data
        {
            get
            {
                return m_data;
            }
        }
        public string OutputNodeId
        {
            get
            {
                return m_outputPoint.NodeId;
            }
        }

        public string InputNodeId
        {
            get
            {
                return m_inputPoint.NodeId;
            }
        }

        public ConnectionPointData OutputPoint
        {
            get
            {
                return m_outputPoint;
            }
        }

        public ConnectionPointData InputPoint
        {
            get
            {
                return m_inputPoint;
            }
        }


        public ConnectionGUIInspectorHelper Inspector
        {
            get
            {
                if (m_inspector == null) {
                    m_inspector = ScriptableObject.CreateInstance<ConnectionGUIInspectorHelper>();
                    m_inspector.hideFlags = HideFlags.DontSave;
                }
                m_inspector.UpdateInspector(this);
                return m_inspector;
            }
        }

        public bool IsSelected
        {
            get
            {
                return (m_inspector != null && Selection.activeObject == m_inspector && m_inspector.connectionGUI == this);
            }
        }

        private Rect m_buttonRect;

        public static ConnectionGUI LoadConnection(ConnectionData data, ConnectionPointData output, ConnectionPointData input)
        {
            return new ConnectionGUI(
                data,
                output,
                input
            );
        }

        public static ConnectionGUI CreateConnection(string type,ConnectionPointData output, ConnectionPointData input)
        {
            var connection = NodeConnectionUtility.CustomConnectionTypes.Find(x => x.connection.Name == type).CreateInstance();
            return new ConnectionGUI(
                new ConnectionData(type, connection, output, input),
                output,
                input
            );
        }

        private ConnectionGUI(ConnectionData data, ConnectionPointData output, ConnectionPointData input)
        {
            //UnityEngine.Assertions.Assert.IsTrue(output.IsOutput, "Given Output point is not output.");
            //UnityEngine.Assertions.Assert.IsTrue(input.IsInput, "Given Input point is not input.");

            m_inspector = ScriptableObject.CreateInstance<ConnectionGUIInspectorHelper>();
            m_inspector.hideFlags = HideFlags.DontSave;

            this.m_data = data;
            this.m_outputPoint = output;
            this.m_inputPoint = input;
        }

        public Rect GetRect()
        {
            return m_buttonRect;
        }

        public bool IsValid(List<NodeGUI> nodes)
        {
            var startNode = nodes.Find(node => node.Id == OutputNodeId);
            if (startNode == null)
            {
                return false;
            }

            var endNode = nodes.Find(node => node.Id == InputNodeId);
            if (endNode == null)
            {
                return false;
            }

            if (nodes.Find(x => x.Data.OutputPoints.Find(y => y.Id == m_data.FromNodeConnectionPointId) != null) == null)
            {
                return false;
            }

            if (nodes.Find(x => x.Data.InputPoints.Find(y => y.Id == m_data.ToNodeConnectionPointId) != null) == null)
            {
                return false;
            }

            return true;
        }

        public void DrawConnection(List<NodeGUI> nodes)
        {
            var startNode = nodes.Find(node => node.Id == OutputNodeId);
            if (startNode == null)
            {
                return;
            }

            var endNode = nodes.Find(node => node.Id == InputNodeId);
            if (endNode == null)
            {
                return;
            }


            var startPoint = m_outputPoint.GetGlobalPosition(startNode.Region);

            var endPoint = m_inputPoint.GetGlobalPosition(endNode.Region);

            var centerPoint = startPoint + ((endPoint - startPoint) / 2);

            var pointDistanceX = NGEditorSettings.GUI.CONNECTION_CURVE_LENGTH;

            var startTan = new Vector3(startPoint.x + pointDistanceX, centerPoint.y, 0f);

            var endTan = new Vector3(endPoint.x - pointDistanceX, centerPoint.y, 0f);

            //用于批量选中曲线
            m_buttonRect = new Rect(centerPoint.x - 10, centerPoint.y - 10, 20, 20);
            //绘制曲线
            DrawCurve(startPoint, endPoint, startTan, endTan);
            //处理右键事件
            HandleClick(startPoint, endPoint, startTan, endTan);
            //自定义绘制
            connectionDrawer.OnConnectionGUI(startPoint, endPoint, startTan, endTan);
            //绘制标签
            connectionDrawer.OnDrawLabel(centerPoint, ConnectionType);
        }
        private void DrawCurve(Vector2 startV3, Vector2 endV3, Vector2 startTan, Vector2 endTan)
        {
            Color lineColor;
            var lineWidth = connectionDrawer == null ? 3 : connectionDrawer.LineWidth;// (totalAssets > 0) ? 3f : 2f;

            if (IsSelected)
            {
                lineColor = NGEditorSettings.GUI.COLOR_ENABLED;
            }
            else
            {
                lineColor = connectionDrawer == null ? Color.gray : connectionDrawer.LineColor;
            }

            ConnectionGUIUtility.HandleMaterial.SetPass(0);

            Handles.DrawBezier(startV3, endV3, startTan, endTan, lineColor, null, lineWidth);
        }

        private void HandleClick(Vector2 startV3, Vector2 endV3, Vector2 startTan, Vector2 endTan)
        {
            var bezierPoses = Handles.MakeBezierPoints(startV3, endV3, startTan, endTan, (int)(Vector3.Distance(startV3, endV3) * 0.1f));

            if ((Event.current.type == EventType.MouseUp && Event.current.button == 0))
            {
                if (ClickedOnBezier(bezierPoses))
                {
                    ConnectionGUIUtility.ConnectionEventHandler(new ConnectionEvent(ConnectionEvent.EventType.EVENT_CONNECTION_TAPPED, this));
                    Event.current.Use();
                }

            }

            else if (Event.current.type == EventType.ContextClick || (Event.current.type == EventType.MouseUp && Event.current.button == 1))
            {
                if (ClickedOnBezier(bezierPoses))
                {
                    var menu = new GenericMenu();

                    if (connectionDrawer != null)
                    {
                        connectionDrawer.OnContextMenuGUI(menu, this);
                    }

                    menu.AddItem(
                        new GUIContent("Delete"),
                        false,
                        () =>
                        {
                            Delete();
                        }
                    );
                    menu.ShowAsContext();
                    Event.current.Use();
                }
            }
        }

        private bool ClickedOnBezier(Vector3[] postions)
        {
            var clickPos = Event.current.mousePosition;
            var smallDistence = postions.Select(x => Vector3.Distance(x, clickPos)).Min();
            return smallDistence < 10;
        }

        public bool IsEqual(ConnectionPointData from, ConnectionPointData to)
        {
            return (m_outputPoint == from && m_inputPoint == to);
        }


        public void SetActive(bool active)
        {
            if (active){
                Selection.activeObject = Inspector;
            }
        }
        public void DrawObject()
        {
            EditorGUI.BeginChangeCheck();
            if (connectionDrawer != null)
                connectionDrawer.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(Data.Object);
            }
        }
        public void Delete()
        {
            ConnectionGUIUtility.ConnectionEventHandler(new ConnectionEvent(ConnectionEvent.EventType.EVENT_CONNECTION_DELETED, this));
        }

       
    }

    public static class NodeEditor_ConnectionListExtension
    {
        public static bool ContainsConnection(this List<ConnectionGUI> connections, ConnectionPointData output, ConnectionPointData input)
        {
            foreach (var con in connections)
            {
                if (con.IsEqual(output, input))
                {
                    return true;
                }
            }
            return false;
        }
    }
}