using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static event Action<Vector2> MoveInput;
    public static event Action<bool> SprintInput;
    public static event Action InteractInput;
    public static event Action SkipInput;

    void OnMove(InputValue p_value)
    {
        MoveInput?.Invoke(p_value.Get<Vector2>().normalized);
    }

    void OnSprint(InputValue p_value)
    {
        bool l_sprinting;

        if (p_value.Get<float>() > .5f) l_sprinting = true;
        else l_sprinting = false;

        SprintInput?.Invoke(l_sprinting);
    }

    void OnInteract(InputValue p_value)
    {
        InteractInput?.Invoke();
    }

    void OnSkip(InputValue p_value)
    {
        SkipInput?.Invoke();
    }
}
