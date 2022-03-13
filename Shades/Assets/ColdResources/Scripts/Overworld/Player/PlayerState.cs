using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [HideInInspector] public Direction Dir = Direction.South;
    [HideInInspector] public bool Moving = false;
    [HideInInspector] public bool Attacking = false;
    [HideInInspector] public bool Rolling = false;
    [HideInInspector] public bool Talking = false;
    [HideInInspector] public Vector3 MoveDir;
    [HideInInspector] public Vector3 AttackMomentumDir = Vector3.down;
    [HideInInspector] public float AttackMomentumStrength = 10;
    [HideInInspector] public float AttackProgression = 0;
    [HideInInspector] public bool CanQueueAttack = true;
    [HideInInspector] public AnimationCurve AttackMomentumCurve;
}
