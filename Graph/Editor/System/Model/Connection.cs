using UnityEngine;
using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

using Model = NodeGraph.DataModel;

namespace NodeGraph {

    public abstract class Connection
    {
        internal virtual int LineWidth { get { return 3; } }

        internal virtual Color LineColor { get { return Color.white; } }

        internal virtual string Label { get { return ""; } }

        internal abstract void OnInspectorGUI(ConnectionGUI con, ConnectionGUIEditor connectionGUIEditor, Action onChanged);
    }
}