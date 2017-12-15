using UnityEngine;
using NodeGraph;
using System;
using NodeGraph.DataModel;

[CustomConnection(KeyConst.BridgeName)]
public class BridgeConnection : Connection
{
    public string textString;
}
