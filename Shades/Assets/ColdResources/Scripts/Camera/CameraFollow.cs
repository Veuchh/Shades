using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] bool _followXAxis;
    [SerializeField] bool _followYAxis;
    Transform _playerTf;

    void Awake()
    {
        _playerTf = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (_followXAxis) transform.position = new Vector3(_playerTf.position.x, transform.position.y, transform.position.z);
        if (_followYAxis) transform.position = new Vector3(transform.position.x, _playerTf.position.y, transform.position.z);
    }
}
