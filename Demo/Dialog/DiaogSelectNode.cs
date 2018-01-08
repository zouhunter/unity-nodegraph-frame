using UnityEngine;
using System.Collections.Generic;
using NodeGraph;
using NodeGraph.DataModel;
[CustomNode("Dialog/Select", 0, "Dialog")]
public class DialogSelectNode : Node
{
    public string person;
    public List<string> infomation;

    protected override IEnumerable<Point> inPoints
    {
        get
        {
            return new Point[] { new Point("->", "", 1) };
        }
    }
}
