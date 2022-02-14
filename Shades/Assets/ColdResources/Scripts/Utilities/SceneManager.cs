using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SceneAsset _menuScene;

    public static event Action SceneStartedLoading;
    public static event Action SceneFinishedLoading;

    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_menuScene.name, LoadSceneMode.Single);
    }

    private void Awake()
    {
        SceneChangeTrigger.ChangeToScene += ChangeScene;
    }

    private void OnDestroy()
    {
        SceneChangeTrigger.ChangeToScene -= ChangeScene;
    }

    void ChangeScene(string p_scene)
    {
        StartCoroutine(ChangeRoom(p_scene));
    }

    IEnumerator ChangeRoom(string p_roomName)
    {
        SceneStartedLoading?.Invoke();

        //wait until screen is Fully black
        yield return new WaitForSeconds(.1f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(p_roomName, LoadSceneMode.Single);

        while (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != p_roomName)
        {
            yield return null;
        }

        SceneFinishedLoading?.Invoke();
    }
}
