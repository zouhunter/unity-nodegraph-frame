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
using NodeGraph;
using NodeGraph.DataModel;

[CustomNode("Luncher", 0)]
public class Luncher : Node
{
    public override string NodeOutputType
    {
        get
        {
            return KeyConst.BridgeName;
        }
    }
    
    public override Node Clone(NodeData newData)
    {
        var newNode = new Luncher();
        Debug.Log("Clone");
        return newNode;
    }

}