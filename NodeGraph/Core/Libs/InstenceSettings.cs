using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using NodeGraph;

namespace NodeGraph
{
    public class InstenceSettings
    {
        /*
			if true, ignore .meta files inside NodeGraph.DataModel.
		*/
        public const bool IGNORE_META = true;

        public const string GRAPH_SEARCH_CONDITION = "t:NodeGraph.DataModel.ConfigGraph";

        public const string GUI_TEXT_SETTINGTEMPLATE_MODEL = "Model";
        public const string GUI_TEXT_SETTINGTEMPLATE_AUDIO = "Audio";
        public const string GUI_TEXT_SETTINGTEMPLATE_TEXTURE = "Texture";
        public const string GUI_TEXT_SETTINGTEMPLATE_VIDEO = "Video";

        public const string UNITY_METAFILE_EXTENSION = ".meta";
        public const string DOTSTART_HIDDEN_FILE_HEADSTRING = ".";
        public const string MANIFEST_FOOTER = ".manifest";
        public const char UNITY_FOLDER_SEPARATOR = '/';// Mac/Windows/Linux can use '/' in Unity.

        public const string BASE64_IDENTIFIER = "B64|";

        public const char KEYWORD_WILDCARD = '*';



        public class Path
        {
            private static string s_basePath;

            public static string BasePath
            {
                get
                {
                    //if (string.IsNullOrEmpty (s_basePath)) {
                    var obj = ScriptableObject.CreateInstance<DataModel.ConfigGraph>();
                    MonoScript s = MonoScript.FromScriptableObject(obj);
                    var configGuiPath = AssetDatabase.GetAssetPath(s);
                    UnityEngine.Object.DestroyImmediate(obj);

                    var fileInfo = new FileInfo(configGuiPath);
                    var baseDir = fileInfo.Directory.Parent.Parent.Parent.Parent;

                    //UnityEngine.Assertions.Assert.AreEqual ("Graph", baseDir.Name);

                    string baseDirPath = baseDir.ToString().Replace('\\', '/');

                    int index = baseDirPath.LastIndexOf(ASSETS_PATH);
                    UnityEngine.Assertions.Assert.IsTrue(index >= 0);

                    s_basePath = baseDirPath.Substring(index);
                    //}
                    return s_basePath;
                }
            }

            public const string ASSETS_PATH = "Assets/";
            public static string GUIResourceBasePath { get { return BasePath + "/Editor/GUI/GraphicResources/"; } }
        }


        public const float WINDOW_SPAN = 20f;

        public const string GROUPING_KEYWORD_DEFAULT = "/Group_*/";
        public const string BUNDLECONFIG_BUNDLENAME_TEMPLATE_DEFAULT = "bundle_*";

        // by default, NodeGraph.DataModel's node has only 1 InputPoint. and 
        // this is only one definition of it's label.
        public const string DEFAULT_INPUTPOINT_LABEL = "-";
        public const string DEFAULT_OUTPUTPOINT_LABEL = "+";
        public const string BUNDLECONFIG_BUNDLE_OUTPUTPOINT_LABEL = "bundles";
        public const string BUNDLECONFIG_VARIANTNAME_DEFAULT = "";

        public const string DEFAULT_FILTER_KEYWORD = "";
        public const string DEFAULT_FILTER_KEYTYPE = "Any";

        public const string FILTER_KEYWORD_WILDCARD = "*";

        public const string NODE_INPUTPOINT_FIXED_LABEL = "FIXED_INPUTPOINT_ID";

        public class GUI
        {
            public const float NODE_BASE_WIDTH = 120f;
            public const float NODE_BASE_HEIGHT = 18f;
            public const float NODE_WIDTH_MARGIN = 48f;
            public const float NODE_TITLE_HEIGHT_MARGIN = 8f;

            public const float CONNECTION_ARROW_WIDTH = 12f;
            public const float CONNECTION_ARROW_HEIGHT = 15f;

            public const float INPUT_POINT_WIDTH = 21f;
            public const float INPUT_POINT_HEIGHT = 29f;

            public const float OUTPUT_POINT_WIDTH = 10f;
            public const float OUTPUT_POINT_HEIGHT = 23f;

            public const float FILTER_OUTPUT_SPAN = 32f;

            public const float CONNECTION_POINT_MARK_SIZE = 16f;

            public const float CONNECTION_CURVE_LENGTH = 20f;

            public const float TOOLBAR_HEIGHT = 20f;
            public const float TOOLBAR_GRAPHNAMEMENU_WIDTH = 150f;
            public const int TOOLBAR_GRAPHNAMEMENU_CHAR_LENGTH = 20;

            public static readonly Color COLOR_ENABLED = new Color(0.43f, 0.65f, 1.0f, 1.0f);
            public static readonly Color COLOR_CONNECTED = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            public static readonly Color COLOR_NOT_CONNECTED = Color.grey;
            public static readonly Color COLOR_CAN_CONNECT = Color.white;//new Color(0.60f, 0.60f, 1.0f, 1.0f);
            public static readonly Color COLOR_CAN_NOT_CONNECT = new Color(0.33f, 0.33f, 0.33f, 1.0f);

            public static string Skin { get { return Path.GUIResourceBasePath + "NodeStyle.guiskin"; } }
            public static string ConnectionPoint { get { return Path.GUIResourceBasePath + "ConnectionPoint.png"; } }
            public static string InputBG { get { return Path.GUIResourceBasePath + "InputBG.png"; } }
            public static string OutputBG { get { return Path.GUIResourceBasePath + "OutputBG.png"; } }
        }
    }
}
