using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections.Generic;
using System.Security.Cryptography;
using NodeGraph;
using NodeGraph.DataModel;

public class UIGraphController : NodeGraphController
{
    protected override void JudgeNodeExceptions(ConfigGraph m_targetGraph, List<NodeException> m_nodeExceptions)
    {
        Debug.Log("JudgeNodeExceptions");
    }
    protected override void BuildFromGraph(ConfigGraph m_targetGraph)
    {
        Debug.Log("BuildFromGraph");
    }

    internal override void OnDragAccept(UnityEngine.Object[] objectReferences)
    {
    }
}
