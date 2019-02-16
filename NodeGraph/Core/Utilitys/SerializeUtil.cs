using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Model = NodeGraph.DataModel;

namespace NodeGraph
{
    /// <summary>
    /// 从JsonGraphUtility简化，只能用于运行时
    /// </summary>
    public class SerializeUtil
    {
        public static List<T> DeserializeGraphs<T>(string json) where T : Model.NodeGraphObj
        {
            List<T> graphs = new List<T>();
            var jsonnode = JSON.Parse(json);
            Debug.Log(jsonnode);

            if (jsonnode.AsArray != null)
            {
                foreach (var node in jsonnode.AsArray)
                {
                    var graph = NodeGraph.SerializeUtil.DeserializeGraph<T>(node.ToString());
                    if(graph != null){
                        graphs.Add(graph);
                    }
                }
            }
            else
            {
                var graph= NodeGraph.SerializeUtil.DeserializeGraph<T>(json);
                if (graph != null)
                {
                    graphs.Add(graph);
                }
            }
            return graphs;
        }

        public static T DeserializeGraph<T>(string json) where T: Model.NodeGraphObj
        {
            var jsonNode = JSONClass.Parse(json);
            var graph = ScriptableObject.CreateInstance<T>();

            json = json.Replace("bool", "Boolean");
            JsonUtility.FromJsonOverwrite(json, graph);

            for (int i = 0; i < jsonNode["m_allNodes"].AsArray.Count; i++)
            {
                var item = jsonNode["m_allNodes"][i]["m_node"];
                var obj = ScriptableObject.CreateInstance(item["type"].Value) as Model.Node;
                JsonUtility.FromJsonOverwrite(item["json"], obj);
                obj.name = obj.GetType().FullName;
                graph.Nodes[i].Object = (obj);
            }


            for (int i = 0; i < jsonNode["m_allConnections"].AsArray.Count; i++)
            {
                var item = jsonNode["m_allConnections"][i]["m_connection"];
                var obj = ScriptableObject.CreateInstance(item["type"].Value) as Model.Connection;
                JsonUtility.FromJsonOverwrite(item["json"], obj);
                obj.name = obj.GetType().FullName;
                graph.Connections[i].Object = (obj);
            }

            return graph;
        }
    }
}