using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerState _state;

    float _baseThreshold = 10f;
    float _southThreshold = 6f;

    float _speed;

    float _walkingSpeed = 50f;
    float _runningSpeed = 100f;

    [SerializeField] AnimationCurve _rollSpeedCurve;

    [SerializeField] LayerMask _collideWith;
    float _rollProgress = 0;
    bool _refreshDir = false;

    private void Awake()
    {
        _state = GetComponent<PlayerState>();
        _speed = _walkingSpeed;
        SubscribeMovement();

        DialogManager.DialogStarted += UnsubscribeMovement;
        DialogManager.DialogEnded += SubscribeMovement;
        SceneChangeTrigger.ChangePlayerPos += OnRoomChanged;
    }

    private void OnDestroy()
    {
        UnsubscribeMovement();

        DialogManager.DialogStarted -= UnsubscribeMovement;
        DialogManager.DialogEnded -= SubscribeMovement;
        SceneChangeTrigger.ChangePlayerPos -= OnRoomChanged;
    }

    void SubscribeMovement()
    {
        InputHandler.LStickInput += Move;
        InputHandler.RBInput += Roll;
        InputHandler.LBInput += Sprint;
    }

    void UnsubscribeMovement()
    {
        InputHandler.LStickInput -= Move;
        InputHandler.RBInput -= Roll;
        InputHandler.LBInput -= Sprint;
    }

    void Sprint(bool p_sprinting)
    {
        if (p_sprinting) _speed = _runningSpeed;
        else _speed = _walkingSpeed;
    }

    void Roll()
    {
        if (_state.CanRoll())
        {
            _state.CurrentMoveDir = _state.GetForwardDir();
            _rollProgress = 0;
            _state.Rolling = true;
            Invoke("StopRoll", _state.RollDuration);
        }
    }

    void StopRoll()
    {
        _state.Rolling = false;
    }

    void Move(Vector2 p_inputDir)
    {
        _state.MoveInput = p_inputDir;
        if (_state.CanMove())
        {
            _state.CurrentMoveDir = p_inputDir;
        }
        if (p_inputDir.sqrMagnitude > 0)
        {
            _refreshDir = true;
            _state.Moving = true;
        }
        else
        {
            _refreshDir = false;
            _state.Moving = false;
        }
    }

    void Update()
    {
        if (CheckForFreeSpace() && !_state.Attacking && !_state.Rolling)
        {
            if (_refreshDir) RefreshDir();
            transform.position += _state.MoveInput * Time.deltaTime * _speed;
            _state.CurrentSpeed = _speed;
            return;
        }
        else if (CheckForFreeSpace() && _state.Attacking)
        {
            transform.position += _state.CurrentMoveDir * Time.deltaTime * _state.AttackMomentumCurve.Evaluate(_state.AttackProgression) * _state.AttackMomentumStrength;
            return;
        }
        else if (CheckForFreeSpace() && _state.Rolling)
        {
            _rollProgress += Time.deltaTime;
            transform.position += _state.CurrentMoveDir * Time.deltaTime * _rollSpeedCurve.Evaluate(_rollProgress) * _state.RollSpeed;
            return;
        }
    }
    void RefreshDir()
    {
        //checks if going sideways
        if (Mathf.Abs(_state.MoveInput.y) < .5f)
        {
            if (_state.MoveInput.x > 0) _state.Dir = Direction.East;
            else _state.Dir = Direction.West;
        }

        else
        {
            if (_state.MoveInput.y > 0) _state.Dir = Direction.North;
            else _state.Dir = Direction.South;
        }
        _refreshDir = false;
    }
    void OnRoomChanged(Vector3 p_newPos)
    {
        StartCoroutine(ChangePosAfterFadeToBlack(p_newPos));
    }

    IEnumerator ChangePosAfterFadeToBlack(Vector3 p_newPos)
    {
        yield return new WaitForSeconds(.1f);
        transform.position = p_newPos;
    }

    bool CheckForFreeSpace()
    {
        bool l_isSpaceFree = true;

        if (_state.Dir == Direction.North || _state.Dir == Direction.South)
        {
            l_isSpaceFree = l_isSpaceFree ? CircleCast(new Vector3(8, 0, 0)) : false;
            l_isSpaceFree = l_isSpaceFree ? CircleCast(new Vector3(-8, 0, 0)) : false;
        }

        else
        {
            l_isSpaceFree = l_isSpaceFree ? CircleCast(new Vector3(0, 5, 0)) : false;
            l_isSpaceFree = l_isSpaceFree ? CircleCast(new Vector3(0, -2.5f, 0)) : false;
        }

        l_isSpaceFree = l_isSpaceFree ? CircleCast(Vector3.zero) : false;

        return l_isSpaceFree;
    }

    bool CircleCast(Vector3 p_offset)
    {
        bool l_isSpaceFree = true;
        float l_usedThreshold;

        if (_state.Dir == Direction.South) l_usedThreshold = _southThreshold;
        else l_usedThreshold = _baseThreshold;


        foreach (var item in Physics2D.CircleCastAll(transform.position + p_offset, .2f, _state.CurrentMoveDir, Time.deltaTime * _state.CurrentSpeed + l_usedThreshold, _collideWith))
        {
            if (item.transform.GetComponent<Collider2D>() && !item.transform.GetComponent<Collider2D>().isTrigger)
            {
                l_isSpaceFree = false;
            }
        }
        return l_isSpaceFree;
    }
}

public enum Direction { North, East, South, West };