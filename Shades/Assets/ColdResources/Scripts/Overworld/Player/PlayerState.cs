using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [HideInInspector] public Direction Dir = Direction.South;
    [HideInInspector] public bool Moving = false;
    [HideInInspector] public bool Attacking = false;
    [HideInInspector] public bool Talking = false;
}
