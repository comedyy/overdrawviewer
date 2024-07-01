
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneDebugViews.asset", menuName = "ASG/SceneDebugViewsAsset")]
public class SceneDebugViewsAsset : ScriptableObject
{
    [Serializable]
    public struct CustomDrawMode
    {
        public string name;
        public string category;
        public Shader shader;
    }

    public List<CustomDrawMode> DebugDrawModes;

    private static SceneDebugViewsAsset cachedInstance;


    public static SceneDebugViewsAsset Instance
    {
        get
        {
            if (cachedInstance == null)
            {
                var path = "Packages/com.xxx.overdrawviewer/Editor/SceneDebugViews.asset";
                cachedInstance = AssetDatabase.LoadAssetAtPath<SceneDebugViewsAsset>(path);
                
                if(cachedInstance == null)
                {
                    Debug.LogError($"not found {path}");
                }
            }
            
            return cachedInstance;
        }
    }
}