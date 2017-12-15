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
using NodeGraph;
using NodeGraph.DataModel;
using System;

[CustomNode("PanelNode", 1)]
public class PanelNode : Node {
    public string key;
    public override string NodeInputType
    {
        get
        {
            return KeyConst.BridgeName;
        }
    }
    public override string NodeOutputType
    {
        get
        {
            return KeyConst.BridgeName;
        }
    }
    public override void Initialize(NodeData data)
    {
        data.AddDefaultInputPoint();
        if(data.InputPoints.Find(x=>x.Label == "aaaaaaaaaaaaaaaaaaaaaaaaa") == null) data.AddInputPoint("aaaaaaaaaaaaaaaaaaaaaaaaa");
        data.AddDefaultOutputPoint();
    }
}
