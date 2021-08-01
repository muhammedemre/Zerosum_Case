using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollowOfficer : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    public Transform target;
    [SerializeField] float startSceneCamMoveDuration, moveSpeedSmoothTime, moveSpeedSmoothTimeHorizontal, zoomSpeed, camStepAmount;
    Vector3 smoothDampRefVector = Vector3.zero;
    Vector3 smoothDampRefHorizontalVector = Vector3.zero;
    public bool follow = true;
    [SerializeField] Vector3 diffVectorBetweenMostZoomInAndOutPositions;
    [SerializeField] Vector3 diffVectorBetweenMostZoomInAndOutRotations;

    [SerializeField] Vector3 diffVectorUnitPosition;
    [SerializeField] Vector3 diffVectorUnitRotation;

    Vector3 mostZoomInPos;
    Vector3 mostZoomOutPos;
    Vector3 mostZoomInRot;
    Vector3 mostZoomOutRot;
    public void StartProcess()
    {
        MeasureDifference();
    }

    public void Follow()
    {
        if (follow && target != null) 
        {
            //FollowHorizontal(); 
            FollowZAndY();
        }
        
        //transform.position = Vector3.SmoothDamp(transform.position, target.position, ref smoothDampRefVector, moveSpeedSmoothTime);
        //transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void FollowHorizontal() 
    {
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothDampRefHorizontalVector, moveSpeedSmoothTimeHorizontal);
    }
    public void FollowZAndY()
    {
        Vector3 targetPos = new Vector3(transform.position.x, target.position.y, target.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothDampRefVector, moveSpeedSmoothTime);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, target.position.z), moveSpeed * Time.deltaTime);
    }

    public void ZoomInAndOut(int camLevel)
    {
        //int camLevel2 = (camLevel > 7) ? 7 : camLevel;
        if (camLevel > 7) 
        {
            camLevel = 7;
            return;
        }
        int camLevel2 = camLevel;
        transform.GetChild(0).DOLocalMove(mostZoomInPos + (diffVectorUnitPosition * camLevel2), zoomSpeed);
        transform.GetChild(0).DOLocalRotate(mostZoomInRot + (diffVectorUnitRotation * camLevel2), zoomSpeed);
        /*int camPosLevel = (brideTailManager.currentSkirtLevel / brideTailManager.skirtAdditionStep);
        transform.GetChild(0).DOLocalMove(camPoses[1] + (diffVectorUnit * ((brideTailManager.currentSkirtLevel > 21) ? 8 : camSkirtLevel)), zoomSpeed);
        transform.GetChild(0).DOLocalRotate(camRots[1] + (diffVectorRotUnit * ((brideTailManager.currentSkirtLevel > 21) ? 8 : camSkirtLevel)), zoomSpeed);*/
    }

    public void RefreshCam()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = CameraManager.Instance.cameraAnchorCameraRotations[CameraManager.CameraPositionsEnum.Start];

        mainCamera.localPosition = CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.Start];
        mainCamera.localEulerAngles = CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.Start];
    }

    public void StartCinematic()
    {       
        Vector3 mostZoomIntPos = CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomIn];
        mainCamera.DOLocalMove(mostZoomIntPos, startSceneCamMoveDuration);
        Vector3 mostZoomInRot = CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomIn];
        mainCamera.DOLocalRotate(mostZoomInRot, startSceneCamMoveDuration * 0.8f);
        Vector3 mostZoomIntRotAnchor = CameraManager.Instance.cameraAnchorCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomIn];
        transform.DORotate(mostZoomIntRotAnchor, startSceneCamMoveDuration);       
    }

    void MeasureDifference() 
    {
        mostZoomInPos = new Vector3(CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomIn].x,
            CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomIn].y,
            CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomIn].z);
        mostZoomOutPos = new Vector3(CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomOut].x,
            CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomOut].y,
            CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PlayMostZoomOut].z);

        mostZoomInRot = new Vector3(CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomIn].x,
            CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomIn].y,
            CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomIn].z);
        mostZoomOutRot = new Vector3(CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomOut].x,
            CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomOut].y,
            CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PlayMostZoomOut].z);


        diffVectorBetweenMostZoomInAndOutPositions = mostZoomOutPos - mostZoomInPos;
        diffVectorBetweenMostZoomInAndOutRotations = mostZoomOutRot - mostZoomInRot;

        diffVectorUnitPosition = diffVectorBetweenMostZoomInAndOutPositions / camStepAmount;
        diffVectorUnitRotation = diffVectorBetweenMostZoomInAndOutRotations / camStepAmount;
    }


    /*public void FinishScene() 
    {
        transform.GetChild(0).DOLocalMove(CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.FinishScene], finishSceneMoveSpeed);
        transform.GetChild(0).DOLocalRotate(CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.FinishScene], finishSceneMoveSpeed);
        transform.DOLocalRotate(CameraManager.Instance.cameraAnchorCameraRotations[CameraManager.CameraPositionsEnum.FinishScene], finishSceneMoveSpeed);
    }*/

    /*public void PodiumZoom() 
    {
        transform.GetChild(0).DOLocalMove(CameraManager.Instance.mainCameraPositions[CameraManager.CameraPositionsEnum.PodiumPos], finishSceneMoveSpeed);
        transform.GetChild(0).DOLocalRotate(CameraManager.Instance.mainCameraRotations[CameraManager.CameraPositionsEnum.PodiumPos], finishSceneMoveSpeed);
    }*/
}
