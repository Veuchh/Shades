using NaughtyAttributes;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField, Scene] int _menuScene;

    public static event Action SceneStartedLoading;
    public static event Action SceneFinishedLoading;

    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_menuScene, LoadSceneMode.Single);
    }

    private void Awake()
    {
        SceneChangeTrigger.ChangeToScene += ChangeScene;
    }

    private void OnDestroy()
    {
        SceneChangeTrigger.ChangeToScene -= ChangeScene;
    }

    void ChangeScene(int p_scene)
    {
        StartCoroutine(ChangeRoom(p_scene));
    }

    IEnumerator ChangeRoom(int p_scene)
    {
        SceneStartedLoading?.Invoke();

        //wait until screen is Fully black
        yield return new WaitForSeconds(.1f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(p_scene, LoadSceneMode.Single);

        while (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != p_scene)
        {
            yield return null;
        }

        SceneFinishedLoading?.Invoke();
    }
}
