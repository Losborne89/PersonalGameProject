using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static Action OnMoveLeft;
    public static Action OnMoveRight;
    public static Action OnJump;
    public static Action OnFeed;
    public static Action OnConfirm;
    public static Action OnOpenDialogue;
    public static Action OnUp;
    public static Action OnDown;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            OnMoveLeft?.Invoke();
        }

        if (Input.GetKey(KeyCode.D))
        {
            OnMoveRight?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
            OnConfirm?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        { 
            OnFeed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOpenDialogue?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUp?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDown?.Invoke();
        }

    }
}
