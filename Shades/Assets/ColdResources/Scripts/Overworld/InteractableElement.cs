using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using NaughtyAttributes;


public class InteractableElement : MonoBehaviour
{
    public static event Action<List<DialogBoxContent>, Action<int>> TextInteractionCallback;

    public bool _hasTextInteraction;

    [SerializeField] [ShowIf("_hasTextInteraction")] List<DialogNode> Dialogs;

    int _interactCount = 0;

    [SerializeField]
    bool _saveInteractions = false;

    [ShowIf("_saveInteractions")]
    [SerializeField]
    string _variableName;

    private void Start()
    {
        if (_saveInteractions)
        {
            _interactCount = (int)SaveManager.Instance.Data.GetType().GetField(_variableName).GetValue(SaveManager.Instance.Data);
        }
    }

    private void OnDestroy()
    {
        if (_saveInteractions) SaveManager.Instance.Data.GetType().GetField(_variableName).SetValue(SaveManager.Instance.Data, _interactCount);
    }

    public virtual void OnInteracted()
    {
        if (_hasTextInteraction && Dialogs.Count > 0)
        {
            TextInteraction();
        }
    }

    private void TextInteraction()
    {
        int l_dialogIndex;

        if (Dialogs.Count <= _interactCount) l_dialogIndex = Dialogs.Count - 1;
        else l_dialogIndex = _interactCount;

        AttachCallbackToContent(DataHandling(Dialogs[l_dialogIndex]));

        _interactCount++;
    }

    List<DialogBoxContent> DataHandling(DialogNode p_data)
    {
        string[] l_lines = p_data.Data.ToString().Split('@');

        List<DialogBoxContent> l_npcDialog = new List<DialogBoxContent>();

        foreach (string l_line in l_lines)
        {
            string[] l_parsedData = l_line.Split('|');

            //TODO : convert text name into image
            Sprite l_contentImage = null;

            float l_textDelay = .03f;


            if (l_parsedData.Length > 2) float.TryParse(l_parsedData[2], NumberStyles.Float, CultureInfo.InvariantCulture, out l_textDelay);

            bool l_skippable = true;

            if (l_parsedData.Length > 3) l_skippable = l_parsedData[3] == "y";

            bool l_choice = false;

            if (l_parsedData.Length > 4) l_choice = l_parsedData[4] == "choose";

            string l_choice0 = "yes";
            string l_choice1 = "no";

            if (l_choice && l_parsedData.Length > 6)
            {
                l_choice0 = l_parsedData[5];
                l_choice1 = l_parsedData[6];
            }

            l_npcDialog.Add(new DialogBoxContent(l_parsedData[1], l_contentImage, l_textDelay, l_skippable, l_choice, l_choice0, l_choice1));
        }

        return l_npcDialog;
    }


    public virtual void OnChoice(int p_choice)
    {
        int l_dialogIndex;
        if (Dialogs.Count <= _interactCount) l_dialogIndex = Dialogs.Count - 1;
        else if (Dialogs.Count == 1) l_dialogIndex = 0;
        else l_dialogIndex = _interactCount--;

       DialogNode l_dialogNode = null;

        if (p_choice == 0) l_dialogNode = Dialogs[l_dialogIndex].Node1;

        else if (p_choice == 1) l_dialogNode = Dialogs[l_dialogIndex].Node2;

        if (l_dialogNode) AttachCallbackToContent(DataHandling(l_dialogNode));
    }

    public virtual void AttachCallbackToContent(List<DialogBoxContent> p_content)
    {
        StartTextInteraction(p_content, OnChoice);
    }

    public virtual void StartTextInteraction(List<DialogBoxContent> p_content, Action<int> p_callback)
    {
        TextInteractionCallback?.Invoke(p_content, p_callback);
    }
}
