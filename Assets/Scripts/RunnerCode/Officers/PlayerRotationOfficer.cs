using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationOfficer : MonoBehaviour
{
    public float previousXPos = 0; // player always starts at Vector3.zero 
    float positionChange;
    [SerializeField] float rotateLimit, smoothTime, rotRate;
    public bool ableToRotate = true;

    public void RotateTheCarAccordingToTheHorizontalSpeed()
    {
        if (ableToRotate)
        {
            MeasureMovementSinceLastFrame();
        }

    }
    void MeasureMovementSinceLastFrame()
    {
        positionChange = transform.position.x - previousXPos;
        previousXPos = transform.position.x;
        RotateTheCar();
    }

    void RotateTheCar()
    {
        float rotY = Mathf.Clamp(positionChange * rotRate, -rotateLimit, rotateLimit);
        Vector3 newEuler = new Vector3(0f, rotY, 0f);
        //print("ROTY : "+ rotY + " NeWEULER : " + newEuler + " POS * ROTRATE " + (positionChange*rotRate));
        transform.eulerAngles = newEuler;
    }
}
