using UnityEngine;
using UnityEditor;


namespace NodeGraph.DefultSkin
{
    public abstract class DefultSkinNodeView : NodeView
    {
        public virtual int Style { get { return 0; } }
        public virtual float CustomNodeHeight { get { return 0; } }
        private int lastSyle;
        private GUISkin _skin;
        protected GUISkin skin
        {
            get
            {
                if (_skin == null)
                {
                    var path = AssetDatabase.GUIDToAssetPath("75ce4a2b9ce8e45f9bcb12d38ed95952");
                    _skin = AssetDatabase.LoadAssetAtPath<GUISkin>(path);
                    Debug.Assert(_skin != null, "the guid of the skin is changed!");
                }
                return _skin;
            }
        }
        private GUIStyle _activeStyle;
        public override GUIStyle ActiveStyle
        {
            get
            {
                if (Style != lastSyle){
                    ResetStyle();
                }
                if (_activeStyle == null)
                {
                    _activeStyle = new GUIStyle(skin.FindStyle(string.Format("node {0} on", Style)));
                }
                return _activeStyle;
            }
        }

        private GUIStyle _inactiveStyle;
        public override GUIStyle InactiveStyle
        {
            get
            {
                if(Style != lastSyle){
                    ResetStyle();
                }
                if(_inactiveStyle == null)
                {
                    _inactiveStyle = new GUIStyle( skin.FindStyle(string.Format("node {0}", Style))) ;
                }
                return _inactiveStyle;
            }
        }

        protected void ResetStyle()
        {
            _activeStyle = null;
            _inactiveStyle = null;
            lastSyle = Style;
        }
    }
}