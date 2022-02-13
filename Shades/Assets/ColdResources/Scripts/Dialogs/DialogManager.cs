using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{
    public static event Action DialogStarted;
    public static event Action DialogEnded;

    public static DialogManager Instance;

    [SerializeField] GameObject _dialogPannel;

    [SerializeField] TextMeshProUGUI _dialogBox;
    bool _isDialogDisplaying = false;

    bool _skipDialog = false;

    Queue<DialogBoxContent> _contentToDisplay;

    private void Awake()
    {
        if (DialogManager.Instance) Destroy(this);
        else DialogManager.Instance = this;

        InteractableElement.TextInteractionCallback += OnTextInteraction;
    }
    private void OnDestroy()
    {
        InteractableElement.TextInteractionCallback -= OnTextInteraction;
    }

    void OnTextInteraction(List<DialogBoxContent> p_contents)
    {
        _dialogPannel.SetActive(true);

        DialogStarted?.Invoke();
        _contentToDisplay = new Queue<DialogBoxContent>();

        InputHandler.InteractInput += OnPlayerPressedInteract;
        InputHandler.SkipInput += OnPlayerPressedSkip;

        foreach (DialogBoxContent l_content in p_contents)
        {
            _contentToDisplay.Enqueue(l_content);
        }

        StartCoroutine(DisplayDialog(_contentToDisplay.Dequeue()));
    }

    void OnPlayerPressedInteract()
    {
        if (_contentToDisplay.Count == 0)
        {
            InputHandler.InteractInput -= OnPlayerPressedInteract;
            InputHandler.SkipInput -= OnPlayerPressedSkip;
            DialogEnded?.Invoke();
            _dialogPannel.SetActive(false);
            return;
        }

        if (!_isDialogDisplaying) StartCoroutine(DisplayDialog(_contentToDisplay.Dequeue()));
    }

    void OnPlayerPressedSkip()
    {
        if (_isDialogDisplaying) _skipDialog = true;
    }

    IEnumerator DisplayDialog(DialogBoxContent p_content)
    {
        //Init
        _isDialogDisplaying = true;
        _skipDialog = false;
        _dialogBox.text = p_content.Text;
        _dialogBox.maxVisibleCharacters = 0;
        //TODO : change text dimensions if there is a portrait

        //needed to refresh _dialogBox.textInfo.characterCount
        yield return null;

        int l_totalVisibleCharacters = _dialogBox.textInfo.characterCount;
        int l_counter = 0;

        //Reveal loop
        while (l_counter < l_totalVisibleCharacters)
        {
            if (_skipDialog == true && p_content.Skippable)
            {
                _dialogBox.maxVisibleCharacters = _dialogBox.textInfo.characterCount;
                _skipDialog = false;
                break;
            }

            l_counter++;
            _dialogBox.maxVisibleCharacters = l_counter;
            yield return new WaitForSeconds(p_content.LetterTiming);
        }
        _isDialogDisplaying = false;
    }
}


public struct DialogBoxContent
{
    public string Text;
    public Sprite Portrait;
    public float LetterTiming;
    public bool Skippable;

    public DialogBoxContent(string p_sentence, Sprite p_portrait = null, float p_letterTiming = .02f, bool p_skippable = true)
    {
        Text = p_sentence;
        Portrait = p_portrait;
        LetterTiming = p_letterTiming;
        Skippable = p_skippable;
    }
}