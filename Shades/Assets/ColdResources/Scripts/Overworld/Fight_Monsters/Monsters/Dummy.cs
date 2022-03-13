using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonsterLogic
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void OnTriggerEnter2D(Collider2D p_col)
    {
        base.PhysicalHit(1);
    }

    protected override void Attack(GameObject p_attack, Transform p_target)
    {
        throw new System.NotImplementedException();
    }

    protected override void SpellHit()
    {
        throw new System.NotImplementedException();
    }

    protected override void Killed()
    {
        
    }

    protected override void Movement()
    {
        throw new System.NotImplementedException();
    }

    
}
