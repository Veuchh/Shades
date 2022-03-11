using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticChangeLayerOrder : MonoBehaviour
{
    SpriteRenderer _rend;

    void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
        _rend.sortingOrder = Mathf.RoundToInt(transform.position.y * -1);   
    }
}
