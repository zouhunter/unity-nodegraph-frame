using UnityEngine;
using System.Collections;
using NodeGraph;
using NodeGraph.DataModel;
using System.Collections.Generic;

[CustomNode("Dialog/Entry", 0, "Dialog")]
public class DialogEntryNode : Node
{
    public string person;
    public string infomation;
    protected override IEnumerable<Point> outPoints
    {
        get
        {
            return new Point[] { new Point("->","",1) };
        }
    }
}
