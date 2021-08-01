using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveOfficer : MonoBehaviour
{
    [SerializeField] PlayerActor playerActor;
    [SerializeField] float forwardMoveSpeed, horizontalMoveSpeed, backForceAmount, haltDelayCuzBackForce, smoothTime;
    [HideInInspector] public float platformWidth;
    public Transform leftBorderForPlayer, rightBorderForPlayer;
    Vector3 dampRefVector = Vector3.zero;
    public bool atBridge = false;
    public void MoveForward()
    {
        if (!PlayerManager.Instance.halt)
        {
            transform.Translate(transform.forward * forwardMoveSpeed * Time.deltaTime);
        }
    }

    public void PlayerHorizontalMoveSlide(float moveRate)
    {
        if (!atBridge) 
        {
            HorizontalMoveWithPositioning(moveRate);
        }        
    }

    void HorizontalMoveWithPositioning(float moveRate) 
    {
        float newXPos = transform.position.x + (moveRate * horizontalMoveSpeed * platformWidth);
        newXPos = Mathf.Clamp(newXPos, leftBorderForPlayer.position.x, rightBorderForPlayer.position.x);
        Vector3 newPos = new Vector3(newXPos, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref dampRefVector, smoothTime);
    }

    public IEnumerator BackForce()
    {
        PlayerManager.Instance.halt = true;
        //print("BACKFORCE AMOUNT : " + (-Vector3.forward * backForceAmount));
        playerActor.playerRb.AddForce(-Vector3.forward * backForceAmount);
        //print("VELOCITY AFTER FORCE "+ playerActor.playerRb.velocity);
        yield return new WaitForSeconds(haltDelayCuzBackForce);
        //print("VELOCITY AFTER WaitForSeconds " + playerActor.playerRb.velocity);
        playerActor.playerRb.velocity = Vector3.zero;
        PlayerManager.Instance.halt = false;
    }
}
