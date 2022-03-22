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
    [HideInInspector] public Vector3 CurrentMoveDir;
    [HideInInspector] public Vector3 MoveInput;
    [HideInInspector] public float AttackMomentumStrength = 10;
    [HideInInspector] public float AttackProgression = 0;
    [HideInInspector] public bool CanQueueAttack = true;
    [HideInInspector] public AnimationCurve AttackMomentumCurve;
    [HideInInspector] public float RollSpeed = 85f;
    [HideInInspector] public float RollDuration = .7f;
    [HideInInspector] public bool AttackInputQueued = false;
    [HideInInspector] public float CurrentSpeed;

    public bool CanMove()
    {
        if (Attacking || Rolling || Talking) return false;
        else return true;
    }

    public bool CanRoll()
    {
        if (Attacking || Rolling || Talking) return false;
        else return true;
    }

    public bool CanAttack()
    {
        if ((Rolling || Talking) || (Attacking && AttackInputQueued)) return false;
        else return true;
    }

    public Vector3 GetForwardDir()
    {
        Vector3 l_momentum = Vector3.zero;

        if (MoveInput.sqrMagnitude != 0) l_momentum = MoveInput;
        else
        {
            switch (Dir)
            {
                case Direction.North:
                    l_momentum = Vector3.up;
                    break;
                case Direction.East:
                    l_momentum = Vector3.right;
                    break;
                case Direction.South:
                    l_momentum = Vector3.down;
                    break;
                case Direction.West:
                    l_momentum = Vector3.left;
                    break;
            }
        }
        return l_momentum;
    }
}
