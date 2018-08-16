using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Model=NodeGraph.DataModel;

namespace NodeGraph {
	public static class NodeGUIUtility {

		public static Texture2D LoadTextureFromFile(string path) {
			Texture2D texture = new Texture2D(1, 1);
			texture.LoadImage(File.ReadAllBytes(path));
			return texture;
		}


		public struct PlatformButton {
			public readonly GUIContent ui;
			public readonly BuildTargetGroup targetGroup;

			public PlatformButton(GUIContent ui, BuildTargetGroup g) {
				this.ui = ui;
				this.targetGroup = g;
			}
		}

		public static Action<NodeEvent> NodeEventHandler {
			get {
				return NodeSingleton.s.emitAction;
			}
			set {
				NodeSingleton.s.emitAction = value;
			}
		}

		public static Texture2D inputPointBG {
			get {
				if(NodeSingleton.s.inputPointBG == null) {
                    NodeSingleton.s.inputPointBG = LoadTextureFromFile(NGEditorSettings.GUI.InputBG);
				}
				return NodeSingleton.s.inputPointBG;
			}
		}

		public static Texture2D outputPointBG {
			get {
				if(NodeSingleton.s.outputPointBG == null) {
                    NodeSingleton.s.outputPointBG = LoadTextureFromFile(NGEditorSettings.GUI.OutputBG);
				}
				return NodeSingleton.s.outputPointBG;
			}
		}

		public static Texture2D pointMark {
			get {
				if(NodeSingleton.s.pointMark == null) {
                    NodeSingleton.s.pointMark = LoadTextureFromFile(NGEditorSettings.GUI.ConnectionPoint);
				}
				return NodeSingleton.s.pointMark;
			}
		}

		public static List<string> allNodeNames {
			get {
				return NodeSingleton.s.allNodeNames;
			}
			set {
				NodeSingleton.s.allNodeNames = value;
			}
		}

		private class NodeSingleton {
			public Action<NodeEvent> emitAction;

			public Texture2D inputPointBG;
			public Texture2D outputPointBG;
			public Texture2D pointMark;

			public List<string> allNodeNames;

			private static NodeSingleton s_singleton;

			public static NodeSingleton s {
				get {
					if( s_singleton == null ) {
						s_singleton = new NodeSingleton();
					}

					return s_singleton;
				}
			}
		}

        public static Rect Muit(this Rect rect,float scaleFactor)
        {
            return new Rect(rect.x * scaleFactor, rect.y * scaleFactor, rect.width * scaleFactor, rect.height * scaleFactor);
        }
	}
}
