﻿using UnityEngine;
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
using UnityEditor;

namespace NodeGraph
{
    public class ScriptableObjUtility
    {
        /// <summary>
        /// Adds the specified hidden subAssets to the mainAsset
        /// </summary>
        public static void SetSubAssets(ScriptableObject[] subAssets, ScriptableObject mainAsset,bool clearOther = false,HideFlags hideFlags = HideFlags.None)
        {
            var path = AssetDatabase.GetAssetPath(mainAsset);
            var oldAssets = AssetDatabase.LoadAllAssetsAtPath(path);

            foreach (ScriptableObject subAsset in subAssets)
            {
                if (subAsset == mainAsset) continue;

                if (System.Array.Find(oldAssets, x => x == subAsset) == null)
                {
                    AddSubAsset(subAsset, mainAsset, hideFlags);
                }
            }

            if(clearOther)
            {
                ClearSubAssets(mainAsset, subAssets);
            }
        }

        /// <summary>
        /// Adds the specified hidden subAsset to the mainAsset
        /// </summary>
        public static void AddSubAsset(ScriptableObject subAsset, ScriptableObject mainAsset,HideFlags hideFlag)
        {
            if (subAsset != null && mainAsset != null)
            {
                UnityEditor.AssetDatabase.AddObjectToAsset(subAsset, mainAsset);
                subAsset.hideFlags = hideFlag;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainAsset"></param>
        public static void ClearSubAssets(ScriptableObject mainAsset, ScriptableObject[] ignores = null)
        {
            if (mainAsset != null)
            {
                var path = AssetDatabase.GetAssetPath(mainAsset);
                var subAssets = AssetDatabase.LoadAllAssetsAtPath(path);


                foreach (var item in subAssets)
                {
                    if (item == mainAsset) continue;

                    if (ignores == null || System.Array.Find(ignores, x => x == item) == null)
                    {
                        Object.DestroyImmediate(item, true);
                    }
                }
            }
        }
    }
}