using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Transform level, startButton;
    LevelActor currentLevelActor;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this); ;
        }
        Instance = this;
    }
    private void Start()
    {      
        CameraManager.Instance.StartProcess();
        CameraManager.Instance.cameraFollowOfficer.StartProcess();
        InputManager.Instance.StartProcess();
        PlayerManager.Instance.StartProcess();

        InstantiateALevel();
    }

    public void LevelInstantiateProcess() 
    {
        startButton.gameObject.SetActive(true);
        currentLevelActor.InstantiateProcess();
        CameraManager.Instance.cameraActor.InitLevel();
    }

    public void GameStartButton() 
    {
        startButton.gameObject.SetActive(false);
        CameraManager.Instance.cameraActor.Game_Start();
        PlayerManager.Instance.Game_Start();
    }

    public void GameReplayButton() 
    {
        PlayerManager.Instance.FinishLevel();
        CameraManager.Instance.cameraFollowOfficer.RefreshCam();
        Destroy(currentLevelActor.gameObject);
        InstantiateALevel();
    }

    void InstantiateALevel() 
    {
        
        Transform tempLevel =  Instantiate(level);
        currentLevelActor = tempLevel.GetComponent<LevelActor>();
    }
}
