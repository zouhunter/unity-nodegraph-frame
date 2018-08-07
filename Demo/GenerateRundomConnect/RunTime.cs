using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using System.Collections.Generic;
using NodeGraph.DataModel;

public class RunTime : MonoBehaviour
{
    public NodeGraphObj graph;
    private Dictionary<string, GameObject> nodeDic = new Dictionary<string, GameObject>();
    private Queue<GameObject> created = new Queue<GameObject>();
    private void OnGUI()
    {
        if (GUILayout.Button("Init"))
        {
            RandomGenerate();
        }
    }

    private void RandomGenerate()
    {
        while (created.Count > 0)
        {
            var item = created.Dequeue();
            GameObject.Destroy(item);
        }
        nodeDic.Clear();

        foreach (var item in graph.Nodes)
        {
            if (item.Object is ObjectNode)
            {
                var node = item.Object as ObjectNode;
                var go = GameObject.CreatePrimitive(node.type);
                created.Enqueue(go);
                go.name = item.Name;
                go.transform.position = Random.insideUnitSphere * 5;
                nodeDic.Add(item.Id, go);
            }

        }

        foreach (var item in graph.Connections)
        {
            var line = new GameObject();
            created.Enqueue(line);
            var render = line.AddComponent<LineRenderer>();
            render.positionCount = (2);
            render.startWidth = render.endWidth = 0.1f;
            render.SetPosition(0, nodeDic[item.FromNodeId].transform.position);
            render.SetPosition(1, nodeDic[item.ToNodeId].transform.position);
        }
    }
}
