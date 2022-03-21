using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class DialogManager : MonoBehaviour
{
    public static event Action DialogStarted;
    public static event Action DialogEnded;

    public static DialogManager Instance;

    [SerializeField] GameObject _dialogPannel;

    [SerializeField] GameObject _choicePannel;

    [SerializeField] TextMeshProUGUI _choice0;

    [SerializeField] TextMeshProUGUI _choice1;

    [SerializeField] TextMeshProUGUI _dialogBox;

    bool _isDialogDisplaying = false;

    bool _skipDialog = false;

    bool _hasChoice;

    Queue<DialogBoxContent> _contentToDisplay;

    Action<int> _callback;

    int _choice;

    private void Awake()
    {
        if (DialogManager.Instance) Destroy(this);
        else DialogManager.Instance = this;

        InteractableElement.TextInteractionCallback += OnTextInteraction;
        InputHandler.LStickInput += OnMenuMove;
    }

    private void OnDestroy()
    {
        InputHandler.LStickInput -= OnMenuMove;
        InteractableElement.TextInteractionCallback -= OnTextInteraction;
    }

    void OnMenuMove(Vector2 p_movement)
    {
        if (Mathf.Abs(p_movement.x) > .5f)
        {
            if (_choice == 0)
            {
                _choice = 1;
                _choice0.color = Color.white;
                _choice1.color = Color.yellow;
            }
            else
            {
                _choice = 0;
                _choice0.color = Color.yellow;
                _choice1.color = Color.white;
            }
        }
    }

    void OnTextInteraction(List<DialogBoxContent> p_contents, Action<int> p_callback)
    {
        _dialogPannel.SetActive(true);

        DialogStarted?.Invoke();
        _contentToDisplay = new Queue<DialogBoxContent>();

        _callback = p_callback;

        InputHandler.AInput += OnPlayerPressedInteract;
        InputHandler.BInput += OnPlayerPressedSkip;

        foreach (DialogBoxContent l_content in p_contents)
        {
            _contentToDisplay.Enqueue(l_content);
        }

        StartCoroutine(DisplayDialog(_contentToDisplay.Dequeue()));
    }

    void OnPlayerPressedInteract()
    {
        //if this is the last box
        if (_contentToDisplay.Count == 0 && !_isDialogDisplaying)
        {
            InputHandler.AInput -= OnPlayerPressedInteract;
            InputHandler.BInput -= OnPlayerPressedSkip;
            DialogEnded?.Invoke();
            _dialogPannel.SetActive(false);
            if (_hasChoice)
            {
                _callback?.Invoke(_choice);
                _callback = null;
                _hasChoice = false;
            }
            return;
        }

        //if this is the last box and there is a choice
        

        //Display next box of dialog is it is done showing
        if (!_isDialogDisplaying)
        {
            StartCoroutine(DisplayDialog(_contentToDisplay.Dequeue()));
        }
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
        _choice = 0;
        _choice0.color = Color.yellow;
        _choice1.color = Color.white;
        _hasChoice = p_content.HasChoice;
        _choicePannel.SetActive(false);
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

        if (_hasChoice)
        {
            _choicePannel.SetActive(true);
            _choice0.text = p_content.Choice0;
            _choice1.text = p_content.Choice1;
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
    public bool HasChoice;
    public string Choice0;
    public string Choice1;
    public DialogBoxContent(string p_sentence, Sprite p_portrait = null, float p_letterTiming = .02f, bool p_skippable = true, bool p_hasChoice = false,
        string p_choice0 = "yes", string p_choice1 = "no")
    {
        Text = p_sentence;
        Portrait = p_portrait;
        LetterTiming = p_letterTiming;
        Skippable = p_skippable;
        HasChoice = p_hasChoice;
        Choice0 = p_choice0;
        Choice1 = p_choice1;
    }
}