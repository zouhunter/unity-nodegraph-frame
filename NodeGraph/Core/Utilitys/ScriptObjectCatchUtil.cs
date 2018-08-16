using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace NodeGraph {
    public class ScriptObjectCatchUtil
    {
        private static Dictionary<string, string> typeCatch = new Dictionary<string, string>();
        private static Dictionary<string, string> jsonCatch = new Dictionary<string, string>();

        public static void Catch(string id,ScriptableObject data)
        {
            if (data != null){
                Catch(id, data.GetType().FullName, JsonUtility.ToJson(data));
            }
        }
        public static ScriptableObject Revert(string id)
        {
            try
            {
                var json = GetJson(id);
                var type = GetType(id);
                if (!string.IsNullOrEmpty(json) && !string.IsNullOrEmpty(type))
                {
                    var m_node = ScriptableObject.CreateInstance(type);
                    JsonUtility.FromJsonOverwrite(json, m_node);
                    return m_node;
                }
                return null;
            }
            catch (System.Exception)
            {
                throw;
            }
           
        }

        private static string GetType(string hash)
        {
            string type = null;
            typeCatch.TryGetValue(hash, out type);
            return type;
        }
        private static string GetJson(string hash)
        {
            string json = null;
            jsonCatch.TryGetValue(hash, out json);
            return json;
        }
        private static void Catch(string hash, string type, string json)
        {
            typeCatch[hash] = type;
            jsonCatch[hash] = json;
        }
    }
}