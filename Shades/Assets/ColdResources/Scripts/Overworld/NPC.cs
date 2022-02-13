using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class NPC : InteractableElement
{
    [SerializeField] TextAsset Dialog;
   
    

    public override void OnInteracted()
    {
        base.OnInteracted();

        string[] l_lines = Dialog.ToString().Split('@');

        List<DialogBoxContent> l_npcDialog = new List<DialogBoxContent>();

        foreach (string l_line in l_lines)
        {
            string[] l_parsedData = l_line.Split('|');

            //TODO : convert text name into image
            Sprite l_contentImage = null;

            float l_textDelay = .05f;

            
            if (l_parsedData.Length > 2) float.TryParse(l_parsedData[2], NumberStyles.Float, CultureInfo.InvariantCulture, out l_textDelay);

            bool l_skippable = true;

            if (l_parsedData.Length > 3) Debug.Log(l_parsedData[3]);
            if (l_parsedData.Length > 3) l_skippable = l_parsedData[3] == "y";

            l_npcDialog.Add(new DialogBoxContent(l_parsedData[1], l_contentImage, l_textDelay, l_skippable));
        }

        StartTextInteraction(l_npcDialog);
    }
}
