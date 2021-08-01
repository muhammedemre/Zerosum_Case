using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActor : MonoBehaviour
{
    public Transform platformBorderRight, platformBorderLeft;
    public Transform player;

    private void Start()
    {
        StartCoroutine(StartDelayer());
    }
    IEnumerator StartDelayer() 
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.LevelInstantiateProcess();
    }

    public void InstantiateProcess()
    {
        _AssignPlayerActorPlayerManager();
        _AssignGameLevelActorPlayerActor();
        _AssignPlayerCameraFollowOfficer();
        _AssignBorders();
        _AssignPlatformWidth();
        _AssignPlayerActorInputManager();
    }

    void _AssignPlayerActorPlayerManager()
    {
        PlayerManager.Instance.playerActor = player.GetComponent<PlayerActor>();
        PlayerManager.Instance.ReadyToPlayCheck(PlayerManager.ReadyToPlayEnum.playerManager_playerActor);
    }
    void _AssignGameLevelActorPlayerActor()
    {
        PlayerManager.Instance.playerActor.levelActor = this;
        PlayerManager.Instance.ReadyToPlayCheck(PlayerManager.ReadyToPlayEnum.playerActor_gameLevelActor);
    }
    void _AssignPlayerCameraFollowOfficer()
    {
        CameraManager.Instance.cameraFollowOfficer.target = player;
        PlayerManager.Instance.ReadyToPlayCheck(PlayerManager.ReadyToPlayEnum.cameraFollow_target);
    }
    void _AssignBorders()
    {
        PlayerManager.Instance.playerActor.playerMoveOfficer.leftBorderForPlayer = platformBorderLeft;
        PlayerManager.Instance.playerActor.playerMoveOfficer.rightBorderForPlayer = platformBorderRight;
        PlayerManager.Instance.ReadyToPlayCheck(PlayerManager.ReadyToPlayEnum.playerMoveOfficer_Borders);
    }
    void _AssignPlatformWidth()
    {
        float diffXBetweenBorders = platformBorderRight.position.x - platformBorderLeft.position.x;
        PlayerManager.Instance.playerActor.playerMoveOfficer.platformWidth = diffXBetweenBorders;
        PlayerManager.Instance.ReadyToPlayCheck(PlayerManager.ReadyToPlayEnum.playerMoveOfficer_platformWidth);
    }
    void _AssignPlayerActorInputManager()
    {
        InputManager.Instance.playerActor = player.GetComponent<PlayerActor>();
        PlayerManager.Instance.ReadyToPlayCheck(PlayerManager.ReadyToPlayEnum.inputManager_playerActor);
    }
}
