using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class CameraManager : MonoBehaviour
{
    [Serializable]
    public class CameraPositionEnumVector3Dict : SerializableDictionary<CameraPositionsEnum, Vector3> { }
    public enum CameraPositionsEnum 
    {
        Start, PlayMostZoomIn, PlayMostZoomOut, FinishScene, PodiumPos
    }

    public static CameraManager Instance;

    [SerializeField] CameraDataContainer cameraDataContainer;
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform cameraAnchor;
    public CameraFollowOfficer cameraFollowOfficer;
    public CameraActor cameraActor;
    public CameraPositionEnumVector3Dict mainCameraPositions = new CameraPositionEnumVector3Dict();
    public CameraPositionEnumVector3Dict mainCameraRotations = new CameraPositionEnumVector3Dict();
    public CameraPositionEnumVector3Dict cameraAnchorCameraRotations = new CameraPositionEnumVector3Dict();

    [SerializeField] CameraPositionsEnum camState;
    
    void Awake() 
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        print(Instance.gameObject.name + " is created");
    }

    public void StartProcess() 
    {
        LoadCameraData();
        cameraFollowOfficer.RefreshCam();
    }

    void SaveMainCamSpecs(CameraPositionsEnum camState) 
    {
        DictContainCheck(mainCameraPositions, camState);
        DictContainCheck(mainCameraRotations, camState);
        mainCameraPositions[camState] = mainCamera.localPosition;
        mainCameraRotations[camState] = mainCamera.localEulerAngles;
    }
    void SaveCameraAnchorSpecs(CameraPositionsEnum camState) 
    {
        DictContainCheck(cameraAnchorCameraRotations, camState);
        cameraAnchorCameraRotations[camState] = cameraAnchor.eulerAngles;
    }

    void DictContainCheck(CameraPositionEnumVector3Dict dict, CameraPositionsEnum camState) 
    {
        if (!dict.ContainsKey(camState))
        {
            dict.Add(camState, new Vector3());
        }
    }

    void SaveCameraData() 
    {
        cameraDataContainer.mainCameraPoses = mainCameraPositions;
        cameraDataContainer.mainCameraRots = mainCameraRotations;
        cameraDataContainer.cameraAnchorCameraRots = cameraAnchorCameraRotations;
    }

    void LoadCameraData() 
    {
        mainCameraPositions = cameraDataContainer.mainCameraPoses;
        mainCameraRotations = cameraDataContainer.mainCameraRots;
        cameraAnchorCameraRotations = cameraDataContainer.cameraAnchorCameraRots;
    }

    #region Button function
    public void ButtonCameraPositioner()
    {
        SaveMainCamSpecs(camState);
        SaveCameraAnchorSpecs(camState);
        SaveCameraData();
    }
    #endregion

}

