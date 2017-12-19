using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
namespace NodeGraph
{
    public class DefultDrawer
    {
        protected Dictionary<FieldInfo, List<FieldInfo>> fieldDic;
        protected Dictionary<FieldInfo, bool> toggleDic;
        protected void OnInspectorGUI(FieldInfo field,object target)
        {
            if (fieldDic == null)
            {
                fieldDic = new Dictionary<FieldInfo, List<FieldInfo>>();
                toggleDic = new Dictionary<FieldInfo, bool>();
                UserDefineUtility.GetNeedSerializeField(field, target, toggleDic, fieldDic);
            }
            if (fieldDic != null && toggleDic != null)
            {
                UserDefineUtility.DrawClassObject(field,target, toggleDic, fieldDic);
            }
        }
    }
}