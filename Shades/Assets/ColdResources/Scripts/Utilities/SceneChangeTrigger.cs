using System;
using UnityEditor;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] SceneAsset _targetScene;
    [SerializeField] Vector2 _targetPlayerPos;

    public static event Action<string> ChangeToScene;
    public static event Action<Vector3> ChangePlayerPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeToScene?.Invoke(_targetScene.name);
            ChangePlayerPos?.Invoke(new Vector3(_targetPlayerPos.x, _targetPlayerPos.y, 0));
        }
    }
}
