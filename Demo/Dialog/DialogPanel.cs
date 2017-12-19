using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
using System;

public class DialogPanel : MonoBehaviour
{
    public ConfigGraph graph;
    private NodeData currentNode;
    public Text person;
    public Text nomal;
    public DialogSeletor selecter;
    void Awake()
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(OnNormalCallBack);
        nomal.GetComponent<EventTrigger>().triggers.Add(entry);

        selecter.callback.AddListener(OnSelectCallBack);
    }

    private void OnNormalCallBack(BaseEventData arg0)
    {
        Debug.Log("callBack");
        if(currentNode.OutputPoints.Count > 0)
        {
            var id = currentNode.OutputPoints[0].Id;
            var bridge = graph.Connections.Find(x => x.FromNodeConnectionPointId == id);
            if(bridge != null)
            {
                currentNode = graph.Nodes.Find(x => x.Id == bridge.ToNodeId);
            }
            else
            {
                currentNode = null;
            }
        }
        else
        {
            currentNode = null;
        }

        if (currentNode != null)
        {
            DisplayNode(currentNode.Operation.Object);
        }
        else
        {
            ReStartDialog();
        }
    }
    private void OnSelectCallBack(int index)
    {
        if (currentNode.OutputPoints.Count > index)
        {
            var id = currentNode.OutputPoints[index].Id;
            var bridge = graph.Connections.Find(x => x.FromNodeConnectionPointId == id);
            currentNode = graph.Nodes.Find(x => x.Id == bridge.ToNodeId);
            if (currentNode != null)
            {
                DisplayNode(currentNode.Operation.Object);
            }
            else
            {
                ReStartDialog();
            }
        }
    }

    void OnEnable()
    {
        ReStartDialog();
    }

    void ReStartDialog()
    {
        currentNode = graph.Nodes.Find(x => { Debug.Log(x.Operation.Object.GetType()); return x.Operation.Object.GetType() == typeof(DialogEntryNode); });
        if (currentNode == null)
        {
            Debug.LogError("No Entry");
        }
        else
        {
            DisplayNode(currentNode.Operation.Object);
        }
    }
    void DisplayNode(Node node)
    {
        if(node is DialogEntryNode)
        {
            DisplayNode(node as DialogEntryNode);
        }
        else
        {
            DisplaySelector(node as DialogSelectNode);
        }
    }
    void DisplayNode(DialogEntryNode node)
    {
        SwithchState(true);
        person.text = node.person;
        nomal.text = node.infomation;
    }
    void DisplaySelector(DialogSelectNode node)
    {
        SwithchState(false);
        person.text = node.person;
        selecter.ChargeSelecton(node.infomation);
    }
    void SwithchState(bool isNormal)
    {
        selecter.gameObject.SetActive(!isNormal);
        nomal.gameObject.SetActive(isNormal);
    }
}
