using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraDataContainer", menuName = "Camera/CameraData", order = 1)]
public class CameraDataContainer : ScriptableObject
{
    public CameraManager.CameraPositionEnumVector3Dict mainCameraPoses = new CameraManager.CameraPositionEnumVector3Dict();
    public CameraManager.CameraPositionEnumVector3Dict mainCameraRots = new CameraManager.CameraPositionEnumVector3Dict();
    public CameraManager.CameraPositionEnumVector3Dict cameraAnchorCameraRots = new CameraManager.CameraPositionEnumVector3Dict();
   
}
