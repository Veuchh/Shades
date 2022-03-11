using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    [SerializeField] protected MonsterParams _params;
    [HideInInspector] public int HP;
    public List<GameObject> MonsterAttacks { get; private set;}

    public void InitData()
    {
        HP = _params.MaxHP;
        MonsterAttacks = _params.Attacks;
    }
}
