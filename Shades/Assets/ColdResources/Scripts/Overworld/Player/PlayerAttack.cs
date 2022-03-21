using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerState _state;
    [SerializeField] int _maxCombo;

    [SerializeField] List<PlayerAttackData> _comboList;

    [SerializeField] GameObject _NorthCol;
    [SerializeField] GameObject _SouthCol;
    [SerializeField] GameObject _EastCol;
    [SerializeField] GameObject _WestCol;

    int _currentCombo = 0;
    

    private void Awake()
    {
        _state = GetComponent<PlayerState>();
        InputHandler.XInput += Attack;
    }

    private void OnDestroy()
    {
        InputHandler.XInput -= Attack;
    }

    private void Update()
    {
        if (_state.CanAttack() && _state.AttackInputQueued && _currentCombo < _maxCombo)
        {
            StartCoroutine(AttackRoutine());
        }

        else if (!_state.Attacking)
        {
            _currentCombo = 0;
        }
    }

    public void Attack()
    {
        if (_state.CanQueueAttack && !_state.Rolling) _state.AttackInputQueued = true;
    }

    IEnumerator AttackRoutine()
    {
        _state.AttackInputQueued = false;
        _state.Attacking = true;
        _state.MomentumDir = _state.GetForwardDir();
        _state.AttackMomentumStrength = _comboList[_currentCombo].MomentumStrength;
        _state.CanQueueAttack = false;
        _state.AttackMomentumCurve = _comboList[_currentCombo].MomentumCurve;

        float l_startTime = Time.time;
        while (l_startTime + (_comboList[_currentCombo].SlashTime * _comboList[_currentCombo].QueueTimingProportion) > Time.time)
        {
            yield return null;
            _state.AttackProgression = Mathf.InverseLerp(l_startTime, l_startTime + _comboList[_currentCombo].SlashTime, Time.time);
        }

        EnableDamageCol(true);
        _state.CanQueueAttack = true;

        while (l_startTime + _comboList[_currentCombo].SlashTime > Time.time)
        {
            yield return null;
            _state.AttackProgression = Mathf.InverseLerp(l_startTime, l_startTime + _comboList[_currentCombo].SlashTime, Time.time);
        }
        EnableDamageCol(false);

        _state.Attacking = false;
        _currentCombo++;
    }

    void EnableDamageCol(bool p_enable)
    {
        _NorthCol.SetActive(false);
        _SouthCol.SetActive(false);
        _EastCol.SetActive(false);
        _WestCol.SetActive(false);

        if (p_enable)
        {
            switch (_state.Dir)
            {
                case Direction.North:
        _NorthCol.SetActive(true);
                    break;
                case Direction.East:
        _EastCol.SetActive(true);
                    break;
                case Direction.South:
        _SouthCol.SetActive(true);
                    break;
                case Direction.West:
        _WestCol.SetActive(true);
                    break;
            }
        }
    }
}

[Serializable]
public class PlayerAttackData
{
    public float SlashTime;
    public float MomentumStrength;
    [Range(0f, 1f)] public float QueueTimingProportion;
    public AnimationCurve MomentumCurve;
    public string AnimationName;
}