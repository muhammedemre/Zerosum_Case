using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    [SerializeField] bool updateButton = false;
    TextSetter textSetter = new TextSetter();
    Data data = new Data();
    void Start()
    {
        StartProcess();
    }


    void Update()
    {
        if (updateButton)
        {
            updateButton = false;
            data.UpdateTrigger();
        }
    }

    void StartProcess()
    {
        data.Attach(textSetter);
    }
}
