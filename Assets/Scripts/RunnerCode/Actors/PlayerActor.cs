using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerActor : MonoBehaviour
{
    public LevelActor levelActor;
    public Rigidbody playerRb;
    public PlayerMoveOfficer playerMoveOfficer;
    public PlayerRotationOfficer playerRotationOfficer;

    void Update()
    {      
        if (!PlayerManager.Instance.halt) 
        {
            PlayerMove();
        }        
    }
    

    void PlayerMove() 
    {
        playerMoveOfficer.MoveForward();
        playerMoveOfficer.PlayerHorizontalMoveSlide(InputManager.Instance.inputSliderActor.moveRate);
        playerRotationOfficer.RotateTheCarAccordingToTheHorizontalSpeed();
    }

    public void HitObstacle() 
    {
        StartCoroutine(playerMoveOfficer.BackForce());
    }
}
