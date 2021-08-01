using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    bool previousMouseState = false;
    public PlayerActor playerActor;
    public InputSliderActor inputSliderActor;

    public static InputManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        print(Instance.gameObject.name + " is created");
    }

    public void StartProcess()
    {

    }

    void FixedUpdate()
    {

        if (!PlayerManager.Instance.halt)
        {
            InputControl();
        }
              
    }
    void InputControl()
    {
        if (Application.isMobilePlatform)
        {
            TouchInput();
        }
        else 
        {
            MouseInput();
        }
    }

    void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (previousMouseState == false)
            {
                previousMouseState = true;
                InputManagerInjector(true, false, false, Input.mousePosition);
            }
            else
            {
                InputManagerInjector(false, true, false, Input.mousePosition);
            }
        }
        else
        {
            if (previousMouseState == true)
            {
                previousMouseState = false;
                InputManagerInjector(false, false, true, Vector2.zero);
            }
        }
    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                InputManagerInjector(true, false, false, Input.mousePosition);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                InputManagerInjector(false, true, false, Input.mousePosition);
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                InputManagerInjector(false, true, false, Input.mousePosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                InputManagerInjector(false, false, true, Vector2.zero);
            }
        }
    }

    void InputManagerInjector(bool touchStart, bool touchMoved, bool touchEnded, Vector2 touchPos)
    {
        if (!PlayerManager.Instance.halt && !PlayerManager.Instance.dead)
        {
            inputSliderActor.ManageInput(touchStart, touchMoved, touchEnded, touchPos);
        }
    }
}

