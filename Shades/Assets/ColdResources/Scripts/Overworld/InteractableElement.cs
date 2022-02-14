using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using NaughtyAttributes;

public class InteractableElement : MonoBehaviour
{
    public static event Action<List<DialogBoxContent>> TextInteractionCallback;

    [SerializeField] bool _hasTextInteraction;

    [SerializeField] [ShowIf("_hasTextInteraction")] List<TextAsset> Dialogs;

    int _interactCount = 0;

    public virtual void OnInteracted()
    {
        if (_hasTextInteraction)
        {
            TextInteraction();
        }
    }

    private void TextInteraction()
    {
        int l_dialogIndex;

        if (Dialogs.Count <= _interactCount) l_dialogIndex = Dialogs.Count - 1;
        else l_dialogIndex = _interactCount;

        string[] l_lines = Dialogs[l_dialogIndex].ToString().Split('@');

        List<DialogBoxContent> l_npcDialog = new List<DialogBoxContent>();

        foreach (string l_line in l_lines)
        {
            string[] l_parsedData = l_line.Split('|');

            //TODO : convert text name into image
            Sprite l_contentImage = null;

            float l_textDelay = .03f;


            if (l_parsedData.Length > 2) float.TryParse(l_parsedData[2], NumberStyles.Float, CultureInfo.InvariantCulture, out l_textDelay);

            bool l_skippable = true;

            if (l_parsedData.Length > 3) Debug.Log(l_parsedData[3]);
            if (l_parsedData.Length > 3) l_skippable = l_parsedData[3] == "y";

            l_npcDialog.Add(new DialogBoxContent(l_parsedData[1], l_contentImage, l_textDelay, l_skippable));
        }

        StartTextInteraction(l_npcDialog);

        _interactCount++;
    }

    public virtual void StartTextInteraction(List<DialogBoxContent> p_content)
    {
        TextInteractionCallback?.Invoke(p_content);
    }
}
