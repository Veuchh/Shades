using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster_", menuName = "ScriptableObjects/Monster", order = 2)]
public class MonsterParams : ScriptableObject
{
    [Header("Infos")]
    [HorizontalLine]
    public string Name;
    public Sprite Sprite;
    public TextAsset ContextReaction;


    [Space][Space]

    [Header("Stats")]
    [HorizontalLine]
    public int MaxHP;

    [Space][Space]
    [Header("Movements & Attacks")]
    [HorizontalLine]
    public MovementType MovementType;


    [Space][Space]
    [Header("Peaceful Problem Solving")]
    [HorizontalLine]
    public List<StateOfMind> PossiblesStateOfMind;
    public List<GameObject> Attacks;

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

public enum MovementType
{
    StandStill,
    Hops
}