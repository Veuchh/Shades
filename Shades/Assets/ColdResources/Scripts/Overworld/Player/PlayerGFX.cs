using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    [SerializeField] Animator _animator;
    string _lastAnim = HELENA_IDLE_SOUTH;

    #region AnimNames
    const string HELENA_IDLE_NORTH = "Helena_Idle_North";
    const string HELENA_IDLE_SOUTH = "Helena_Idle_South";
    const string HELENA_IDLE_EAST = "Helena_Idle_East";
    const string HELENA_IDLE_WEST = "Helena_Idle_West";

    const string HELENA_WALK_NORTH = "Helena_Walk_North";
    const string HELENA_WALK_SOUTH = "Helena_Walk_South";
    const string HELENA_WALK_EAST = "Helena_Walk_East";
    const string HELENA_WALK_WEST = "Helena_Walk_West";
    #endregion

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
                if (p_isMoving) PlayAnimation(HELENA_WALK_NORTH);
                else PlayAnimation(HELENA_IDLE_NORTH);
                    break;
            case Direction.East:
                if (p_isMoving) PlayAnimation(HELENA_WALK_EAST);
                else PlayAnimation(HELENA_IDLE_EAST);
                break;
            case Direction.South:
                if (p_isMoving) PlayAnimation(HELENA_WALK_SOUTH);
                else PlayAnimation(HELENA_IDLE_SOUTH);
                break;
            case Direction.West:
                if (p_isMoving) PlayAnimation(HELENA_WALK_WEST);
                else PlayAnimation(HELENA_IDLE_WEST);
                break;
        }
    }

    void PlayAnimation(string p_anim, bool p_checkPlaying = true)
    {
        if (p_checkPlaying && p_anim != _lastAnim) _animator.Play(p_anim);
        _lastAnim = p_anim;
    }
}
