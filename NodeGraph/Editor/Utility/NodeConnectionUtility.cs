
using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using Model = NodeGraph.DataModel;

namespace NodeGraph
{
    public class NodeConnectionUtility
    {
        private static List<CustomConnectionInfo> _customConnectionTypes;
        public static List<CustomConnectionInfo> CustomConnectionTypes
        {
            get
            {
                if (_customConnectionTypes == null)
                {
                    _customConnectionTypes = BuildCustomConnectionList();
                }
                return _customConnectionTypes;
            }
        }
        private static List<CustomConnectionInfo> BuildCustomConnectionList()
        {
            var list = new List<CustomConnectionInfo>();

            var allNodes = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var nodes = assembly.GetTypes()
                    .Where(t => t != typeof(Connection))
                    .Where(t => typeof(Connection).IsAssignableFrom(t));
                allNodes.AddRange(nodes);
            }

            foreach (var type in allNodes)
            {
                CustomConnection attr =
                    type.GetCustomAttributes(typeof(CustomConnection), false).FirstOrDefault() as CustomConnection;

                if (attr != null)
                {
                    list.Add(new CustomConnectionInfo(type, attr));
                }
            }

            return list;
        }
        private static List<CustomNodeInfo> s_customNodes;
        public static List<CustomNodeInfo> CustomNodeTypes
        {
            get
            {
                if (s_customNodes == null)
                {
                    s_customNodes = BuildCustomNodeList();
                }
                return s_customNodes;
            }
        }
        private static List<CustomNodeInfo> BuildCustomNodeList()
        {
            var list = new List<CustomNodeInfo>();

            var allNodes = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var nodes = assembly.GetTypes()
                    .Where(t => t != typeof(Node))
                    .Where(t => typeof(Node).IsAssignableFrom(t));
                allNodes.AddRange(nodes);
            }

            foreach (var type in allNodes)
            {
                CustomNode attr = type.GetCustomAttributes(typeof(CustomNode), false).FirstOrDefault() as CustomNode;

                if (attr != null)
                {
                    list.Add(new CustomNodeInfo(type, attr));
                }
            }

            list.Sort();

            return list;
        }
        public static bool HasValidCustomNodeAttribute(Type t)
        {
            CustomNode attr =
                t.GetCustomAttributes(typeof(CustomNode), false).FirstOrDefault() as CustomNode;
            return attr != null && !string.IsNullOrEmpty(attr.Name);
        }
        public static string GetNodeGUIName(Node node)
        {
            CustomNode attr =
                node.GetType().GetCustomAttributes(typeof(CustomNode), false).FirstOrDefault() as CustomNode;
            if (attr != null)
            {
                return attr.Name;
            }
            return string.Empty;
        }
        public static string GetNodeGUIName(string className)
        {
            var type = Type.GetType(className);
            if (type != null)
            {
                CustomNode attr =
                    type.GetCustomAttributes(typeof(CustomNode), false).FirstOrDefault() as CustomNode;
                if (attr != null)
                {
                    return attr.Name;
                }
            }
            return string.Empty;
        }
        public static int GetNodeOrderPriority(string className)
        {
            var type = Type.GetType(className);
            if (type != null)
            {
                CustomNode attr =
                    type.GetCustomAttributes(typeof(CustomNode), false).FirstOrDefault() as CustomNode;
                if (attr != null)
                {
                    return attr.OrderPriority;
                }
            }
            return CustomNode.kDEFAULT_PRIORITY;
        }
        public static Node CreateNodeInstance(string assemblyQualifiedName)
        {
            if (assemblyQualifiedName != null)
            {
                var type = Type.GetType(assemblyQualifiedName);

                return (Node)type.Assembly.CreateInstance(type.FullName);
            }
            return null;
        }
    }
}