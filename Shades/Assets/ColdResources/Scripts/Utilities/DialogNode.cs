using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog_", menuName = "ScriptableObjects/DialogNode", order = 1)]
public class DialogNode : ScriptableObject
{
    public TextAsset Data;
    public bool HasChoice;
    [ShowIf("HasChoice")]
    public DialogNode Node1;
    [ShowIf("HasChoice")]
    public DialogNode Node2;
}
