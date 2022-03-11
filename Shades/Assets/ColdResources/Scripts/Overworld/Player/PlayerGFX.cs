using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void Awake()
    {
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

        
        switch (p_dir)
        {
            case Direction.North:
                _animator.SetFloat("XMovement", 0);
                _animator.SetFloat("YMovement", 1);
                break;
            case Direction.East:
                _animator.SetFloat("XMovement", 1);
                _animator.SetFloat("YMovement", 0);
                break;
            case Direction.South:
                _animator.SetFloat("XMovement", 0);
                _animator.SetFloat("YMovement", -1);
                break;
            case Direction.West:
                _animator.SetFloat("XMovement", -1);
                _animator.SetFloat("YMovement", 0);
                break;
            default:
                break;
        }
        
    }
}
