using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerState _state;
    public static Vector3 _moveDir;

    float _baseThreshold = 10f;
    float _southThreshold = 4f;

    float _speed;

    float _walkingSpeed = 50f;
    float _runningSpeed = 100f;

    [SerializeField] LayerMask _collideWith;

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
        InputHandler.MoveInput += Move;
        InputHandler.SprintInput += Sprint;
    }

    void UnsubscribeMovement()
    {
        InputHandler.MoveInput -= Move;
        InputHandler.SprintInput -= Sprint;
    }

    void Sprint(bool p_sprinting)
    {
        if (p_sprinting) _speed = _runningSpeed;
        else _speed = _walkingSpeed;
    }

    void Move(Vector2 p_inputDir)
    {
        _moveDir = p_inputDir;

        if (p_inputDir.sqrMagnitude > 0)
        {
            //checks if going sideways
            if (Mathf.Abs(_moveDir.y) < .5f)
            {
                if (_moveDir.x > 0) _state.Dir = Direction.East;
                else _state.Dir = Direction.West;
            }

            else
            {
                if (_moveDir.y > 0) _state.Dir = Direction.North;
                else _state.Dir = Direction.South;
            }
            _state.Moving = true;
        }
        else _state.Moving = false;
    }

    void Update()
    {
        if (CheckForFreeSpace() && !_state.Attacking)
        {
            transform.position += _moveDir * Time.deltaTime * _speed;
            return;
        }
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

        foreach (var item in Physics2D.CircleCastAll(transform.position + p_offset, .1f, _moveDir, Time.deltaTime * _speed + l_usedThreshold, _collideWith))
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