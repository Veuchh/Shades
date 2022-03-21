using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> LStickInput;
    public static event Action<bool> LBInput, RTInput;
    public static event Action AInput, RBInput, BInput, XInput;

    void OnLStick(InputValue p_value)
    {
        LStickInput?.Invoke(p_value.Get<Vector2>().normalized);
    }

    void OnLB(InputValue p_value)
    {
        bool l_sprinting;

        if (p_value.Get<float>() > .5f) l_sprinting = true;
        else l_sprinting = false;

        LBInput?.Invoke(l_sprinting);
    }

    void OnRT(InputValue p_value)
    {
        bool l_pressed;

        if (p_value.Get<float>() > .5f) l_pressed = true;
        else l_pressed = false;
        Debug.Log(l_pressed);
        RTInput?.Invoke(l_pressed);
    }

    void OnA(InputValue p_value)
    {
        AInput?.Invoke();
    }

    void OnB(InputValue p_value)
    {
        BInput?.Invoke();
    }

    void OnRB(InputValue p_value)
    {
        RBInput?.Invoke();
    }

    void OnX(InputValue p_value)
    {
        XInput?.Invoke();
    }
}
