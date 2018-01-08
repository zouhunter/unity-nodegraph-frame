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

[CustomNode("RundomConnect/Node", 1)]
public class ObjectNode : Node {
    public PrimitiveType type;
    protected override IEnumerable<Point> inPoints
    {
        get
        {
            return new Point[] { new Point("a", "line_connection",1) };
        }
    }
    protected override IEnumerable<Point> outPoints
    {
        get
        {
            return new Point[] { new Point("b", "line_connection", 3) };
        }
    }
}
