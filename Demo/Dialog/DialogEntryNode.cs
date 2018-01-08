using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Dialog/Entry", 0, "Dialog")]
public class DialogEntryNode : Node
{
    public string person;
    public string infomation;
}
