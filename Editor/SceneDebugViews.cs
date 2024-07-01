using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneDebugViews
{
private static readonly Dictionary<SceneView, SceneView.CameraMode> PreviousCameraMode = new Dictionary<SceneView, SceneView.CameraMode>();

    [InitializeOnLoadMethod]
    public static void HookIntoSceneView()
    {
        EditorApplication.delayCall += () =>
        {
            SceneView.ClearUserDefinedCameraModes();

            foreach(var ddm in SceneDebugViewsAsset.Instance.DebugDrawModes)
            {
                if ( !string.IsNullOrEmpty(ddm.name) && !string.IsNullOrEmpty(ddm.category))
                {
                    SceneView.AddCameraMode(ddm.name, ddm.category);
                }
            }

            EditorApplication.update += () =>
            {
                foreach (SceneView view in SceneView.sceneViews)
                {
                    if (!PreviousCameraMode.ContainsKey(view) || PreviousCameraMode[view] != view.cameraMode)
                    {
                        view.SetSceneViewShaderReplace(GetDrawModeShader(view.cameraMode), "");
                    }

                    PreviousCameraMode[view] = view.cameraMode;
                }
            };
        };
    }

    private static Shader GetDrawModeShader(SceneView.CameraMode mode)
    {
        foreach(var ddm in SceneDebugViewsAsset.Instance.DebugDrawModes)
        {
            if (string.IsNullOrEmpty(ddm.name) || mode.name != ddm.name)
            {
                continue;
            }

            Shader shader = ddm.shader;
            if (shader != null)
            {
                return shader;
            }
        }

        return null;
    }

    [MenuItem("GameObject/SceneCamera/Follow", false)]
    public static void FollowCamera()
    {
        var obj = Selection.activeObject as GameObject;
        if ( obj == null)
        {
            return;
        }
        SetSceneCamTransformData(obj.transform.position, obj.transform.rotation);
    }

    static public void SetSceneCamTransformData(Vector3 position, Quaternion rotation)
    {
        var scene_view = UnityEditor.SceneView.lastActiveSceneView;
        scene_view.rotation = rotation;
        scene_view.pivot = position + rotation * new Vector3(0, 0, scene_view.cameraDistance);
        scene_view.Repaint();
    }
}