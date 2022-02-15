using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField, Scene] int _targetScene;
    [SerializeField] Vector2 _targetPlayerPos;

    public static event Action<int> ChangeToScene;
    public static event Action<Vector3> ChangePlayerPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeToScene?.Invoke(_targetScene);
            ChangePlayerPos?.Invoke(new Vector3(_targetPlayerPos.x, _targetPlayerPos.y, 0));
        }
    }
}
