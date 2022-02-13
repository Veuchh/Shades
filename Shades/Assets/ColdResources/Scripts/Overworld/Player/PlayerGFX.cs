using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
        SubscribePlayerAnimations();
    }

    private void OnDestroy()
    {
        UnsubscribePlayerAnimations();
    }

    void SubscribePlayerAnimations()
    {
        PlayerMovement.UpdateGFX += PlayerOrientation;
    }

    void UnsubscribePlayerAnimations()
    {
        PlayerMovement.UpdateGFX -= PlayerOrientation;
    }

    void PlayerOrientation(bool p_isMoving, Direction p_dir)
    {
        _animator.SetBool("Moving", p_isMoving);

        int l_dir = 0;

        switch (p_dir)
        {
            case Direction.North:
                l_dir = 2;
                break;
            case Direction.East:
                l_dir = 3;
                break;
            case Direction.South:
                l_dir = 0;
                break;
            case Direction.West:
                l_dir = 1;
                break;
            default:
                break;
        }

        _animator.SetInteger("Dir", l_dir);

        GetComponent<Animator>().SetTrigger("ChangeAnimation");
    }
}
