using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : InteractableElement
{
    public override void AttachCallbackToContent(List<DialogBoxContent> p_content)
    {
        StartTextInteraction(p_content, OnChoice);
    }

    public override void OnChoice(int p_choice)
    {
        if (p_choice == 0) SaveManager.Instance.SaveGame();

        base.OnChoice(p_choice);
    }

}
