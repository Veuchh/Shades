using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerState _state;
    [SerializeField] float _interactionDist = 1f;
    [SerializeField] LayerMask _interactionMask;
    void Awake()
    {
        _state = GetComponent<PlayerState>();
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

        switch (_state.Dir)
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

        foreach (RaycastHit2D l_hit in Physics2D.RaycastAll(transform.position, l_InteractDir, _interactionDist, _interactionMask))
        {
            if (l_hit.transform.GetComponent<NPC>())
            {
                NPCInteract(l_hit.transform.GetComponent<NPC>());
                return;
            }
                else if (l_hit.transform.GetComponent<InteractableElement>())
            {
                Interact(l_hit.transform.GetComponent<InteractableElement>());
                return;
            }
        }
        Debug.Log("Nothing here");
    }

    void NPCInteract(NPC p_NPC)
    {
        p_NPC.OnInteracted(_state.Dir);
    }

    void Interact(InteractableElement p_element)
    {
        p_element.OnInteracted();
    }
}
