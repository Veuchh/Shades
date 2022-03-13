using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    PlayerState _state;
    [SerializeField] int _maxCombo;
    [SerializeField] float _slashTime;
    [SerializeField] [Range(0f, 1f)] float _queueTimingProportion;

    [SerializeField] GameObject _NorthCol;
    [SerializeField] GameObject _SouthCol;
    [SerializeField] GameObject _EastCol;
    [SerializeField] GameObject _WestCol;

    int _currentCombo = 0;
    bool _inputQueued = false;
    bool _canQueueInput = true;

    private void Awake()
    {
        _state = GetComponent<PlayerState>();
    }
    private void Update()
    {
        if (_inputQueued && !_state.Attacking && _currentCombo < _maxCombo)
        {
            StartCoroutine(AttackRoutine());
        }

        else if (!_state.Attacking)
        {
            _currentCombo = 0;
        }

    }
    public void OnAttack(InputValue p_value)
    {
        if (_canQueueInput) _inputQueued = true;
    }

    IEnumerator AttackRoutine()
    {
        _inputQueued = false;
        _state.Attacking = true;
        _canQueueInput = false;

        _currentCombo++;

        yield return new WaitForSeconds(_slashTime * _queueTimingProportion);
        EnableDamageCol(true);
        _canQueueInput = true;

        yield return new WaitForSeconds(_slashTime * (1f - _queueTimingProportion));
        EnableDamageCol(false);
        _state.Attacking = false;
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
