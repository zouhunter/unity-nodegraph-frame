using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;

[CustomConnection("Number")]
public class OperationConnection : Connection {
    public CalcuteType calcuteType;
}


