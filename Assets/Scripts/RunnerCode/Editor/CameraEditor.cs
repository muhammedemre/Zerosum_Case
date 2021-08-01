using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraManager))]
public class CameraEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CameraManager cameraManager = (CameraManager)target;
        GUILayout.Label("Chose CamState then Click the Button");
        if (GUILayout.Button("Save Camera Stats"))
        {
            CameraManager.Instance.ButtonCameraPositioner();
        }
    }
}
