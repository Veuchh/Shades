using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static Vector3 _moveDir;

    float _baseThreshold = 10f;
    float _southThreshold = 4f;

    float _speed;

    float _walkingSpeed = 50f;
    float _runningSpeed = 100f;

    public Direction _dir;

    public static event Action<bool, Direction> UpdateGFX;

    [SerializeField] LayerMask _collideWith;

    private void Awake()
    {
        _speed = _walkingSpeed;
        _dir = Direction.South;
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

        UpdateGFX?.Invoke(false, _dir);
    }

    void Sprint(bool p_sprinting)
    {
        if (p_sprinting) _speed = _runningSpeed;
        else _speed = _walkingSpeed;
    }

    void Move(Vector2 p_inputDir)
    {
        bool l_moving = true;

        _moveDir = p_inputDir;

        if (p_inputDir.sqrMagnitude > 0)
        {
            //checks if going sideways
            if (Mathf.Abs(_moveDir.y) < .5f)
            {
                if (_moveDir.x > 0) _dir = Direction.East;
                else _dir = Direction.West;
            }

            else
            {
                if (_moveDir.y > 0) _dir = Direction.North;
                else _dir = Direction.South;
            }
        }

        else l_moving = false;

        UpdateGFX?.Invoke(l_moving, _dir);

    }

    void Update()
    {
        if (CheckForFreeSpace())
        {
            transform.position += _moveDir * Time.deltaTime * _speed;
            return;
        }
        else if (true)
        {

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

        if (_dir == Direction.North || _dir == Direction.South)
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

        if (_dir == Direction.South) l_usedThreshold = _southThreshold;
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