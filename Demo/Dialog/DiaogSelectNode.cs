using UnityEngine;
using System.Collections.Generic;
using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Dialog/Select", 0, "Dialog")]
public class DialogSelectNode : Node
{
    public string person;
    public List<string> infomation;

}
