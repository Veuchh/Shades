using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicChangeLayerOrde : MonoBehaviour
{
    SpriteRenderer _rend;

    private void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _rend.sortingOrder = Mathf.RoundToInt(transform.position.y * -1);
    }
}
