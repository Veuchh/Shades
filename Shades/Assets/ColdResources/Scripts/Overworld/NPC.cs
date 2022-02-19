using UnityEngine;

public class NPC : InteractableElement
{
    [SerializeField] SpriteRenderer _NPCImage;
    [SerializeField] Sprite _northSprite;
    [SerializeField] Sprite _southSprite;
    [SerializeField] Sprite _westSprite;
    [SerializeField] Sprite _eastSprite;

    public void OnInteracted(Direction p_dir)
    {
        if (_hasTextInteraction)

        switch (p_dir)
        {
            case Direction.North:
                _NPCImage.sprite = _southSprite;
                break;
            case Direction.East:
                _NPCImage.sprite = _westSprite;
                break;
            case Direction.South:
                _NPCImage.sprite = _northSprite;
                break;
            case Direction.West:
                _NPCImage.sprite = _eastSprite;
                break;
        }

        base.OnInteracted();
    }
}
