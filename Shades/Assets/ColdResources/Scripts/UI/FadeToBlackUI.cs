using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlackUI : MonoBehaviour
{
    [SerializeField] Animator _animator;
    void Awake()
    {
        SceneManager.SceneStartedLoading += FadeIn;
        SceneManager.SceneFinishedLoading += FadeOut;
    }

    private void OnDestroy()
    {
        SceneManager.SceneStartedLoading -= FadeIn;
        SceneManager.SceneFinishedLoading -= FadeOut;
    }

    void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }
}
