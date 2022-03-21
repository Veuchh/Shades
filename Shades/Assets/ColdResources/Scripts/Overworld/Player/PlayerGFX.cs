using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    PlayerState _state;
    [SerializeField] Animator _animator;
    string _lastAnim;

    #region AnimNames
    const string HELENA_IDLE = "Helena_Idle";

    const string HELENA_WALK = "Helena_Walk";

    const string HELENA_ATTACK = "Helena_Attack";

    const string HELENA_ROLL = "Helena_Roll";
    #endregion

    private void Awake()
    {
        _state = GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (_state.Rolling) PlayDirectionalAnimation(HELENA_ROLL);

        else if (_state.Attacking) PlayDirectionalAnimation(HELENA_ATTACK);

        else if(_state.Moving) PlayDirectionalAnimation(HELENA_WALK);

        else PlayDirectionalAnimation(HELENA_IDLE);
    }

    void PlayDirectionalAnimation(string p_anim, bool p_checkPlaying = true)
    {
        switch (_state.Dir)
        {
            case Direction.North:
                p_anim += "_North";
                break;
            case Direction.East:
                p_anim += "_East";
                break;
            case Direction.South:
                p_anim += "_South";
                break;
            case Direction.West:
                p_anim += "_West";
                break;
        }

        PlayAnimation(p_anim, p_checkPlaying);
    }

    void PlayAnimation(string p_anim, bool p_checkPlaying = true)
    {
        if (p_checkPlaying && p_anim != _lastAnim) _animator.Play(p_anim);
        _lastAnim = p_anim;
    }
}
