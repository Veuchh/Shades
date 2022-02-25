using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Spell_", menuName = "ScriptableObjects/Spell", order = 2)]
public class Spell : ScriptableObject
{
    public string Name;
    [MinMaxSlider(0, 30)]
    public Vector2 HealthDamage;
    [Range(0, 10)]
    public int HapinessPoints;
    [Range(0, 10)]
    public int SadnessPoints;
    [Range(0, 10)]
    public int AngerPoints;
    [Range(0, 10)]
    public int FearPoints;
}
