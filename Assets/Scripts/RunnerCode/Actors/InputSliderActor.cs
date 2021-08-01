using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSliderActor : MonoBehaviour
{

    float screenWidth, previousXPos;
    public float moveRate = 0f;
    void Start()
    {
        screenWidth = Screen.width;
    } 
    public void ManageInput(bool touchStart, bool touchMoved, bool touchEnded, Vector2 touchPos)
    {
        if (touchStart)
        {
            previousXPos = touchPos.x;
            MoveRate(touchPos.x);
        }
        else if (touchMoved)
        {
            MoveRate(touchPos.x);
        }
        else if (touchEnded)
        {
            moveRate = 0f;
        }
    }

    void MoveRate(float touchPosX)
    {
        float differenceBetweenPreviousAndCurrentPosX = touchPosX - previousXPos;
        moveRate = differenceBetweenPreviousAndCurrentPosX / screenWidth;
        previousXPos = touchPosX;
    }
}
