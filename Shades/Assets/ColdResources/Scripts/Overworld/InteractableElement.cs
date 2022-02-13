using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElement : MonoBehaviour
{
    public static event Action<List<DialogBoxContent>> TextInteractionCallback;

    public virtual void OnInteracted()
    {
    }

    public virtual void StartTextInteraction(List<DialogBoxContent> p_content)
    {
        TextInteractionCallback?.Invoke(p_content);
    }
}
