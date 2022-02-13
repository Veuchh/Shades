using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerMovement _playerMovement;
    [SerializeField] float _interactionDist = 1f;
    [SerializeField] LayerMask _interactionMask;
    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        SubscribeInteraction();

        DialogManager.DialogStarted += UnsubscribeInteraction;
        DialogManager.DialogEnded += SubscribeInteraction;
    }

    private void OnDestroy()
    {
        UnsubscribeInteraction();

        DialogManager.DialogStarted -= UnsubscribeInteraction;
        DialogManager.DialogEnded -= SubscribeInteraction;
    }

    void SubscribeInteraction()
    {
        InputHandler.InteractInput += OnInteracted;
    }

    void UnsubscribeInteraction()
    {
        InputHandler.InteractInput -= OnInteracted;
    }

    void OnInteracted()
    {
        Vector2 l_InteractDir = new Vector2();

        switch (_playerMovement._dir)
        {
            case Direction.North:
                l_InteractDir = Vector2.up;
                break;
            case Direction.South:
                l_InteractDir = Vector2.down;
                break;
            case Direction.East:
                l_InteractDir = Vector2.right;
                break;
            case Direction.West:
                l_InteractDir = Vector2.left;
                break;
        }

        foreach (RaycastHit2D l_hit in Physics2D.RaycastAll(transform.position + new Vector3(0, -15, 0), l_InteractDir, _interactionDist, _interactionMask))
        {
            if (l_hit.transform.GetComponent<InteractableElement>())
            {
                Interact(l_hit.transform.GetComponent<InteractableElement>());
                return; ;
            }
        }
        Debug.Log("Nothing here");
    }

    void Interact(InteractableElement p_element)
    {
        p_element.OnInteracted();
    }
}