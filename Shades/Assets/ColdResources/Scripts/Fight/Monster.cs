using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_", menuName = "ScriptableObjects/Monster", order = 2)]
public class Monster : ScriptableObject
{
    public string Name;

    public Sprite Sprite;

    public int MaxHP;
    public int MaxFeelings;

    public List<StateOfMind> PossiblesStateOfMind;

    public TextAsset ContextReaction;
}

public enum StateOfMind
{
    Bitter, Sad, Devastated,
    Perplexed, Nostalgic, Melancholic,
    Merry, Happy, Extatic,
    Nervous, Surprised, Amazed,
    Worried, Afraid, Terrorised,
    Anxious, Disgusted, Frenetic,
    Annoyed, Angry, Outraged,
    Dissatisfied, Dismayed, Frustrated,
}