using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraActor : MonoBehaviour
{
    public CameraFollowOfficer cameraFollowOfficer;

    private void Update()
    {
        cameraFollowOfficer.Follow();
    }


    public void InitLevel()
    {     
        cameraFollowOfficer.RefreshCam();
    }
    public void Game_Start()
    {
        cameraFollowOfficer.StartCinematic();
    }
}
