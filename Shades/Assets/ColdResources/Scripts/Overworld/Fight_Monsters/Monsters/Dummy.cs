using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonsterLogic
{
    private void Awake()
    {
        Init();
    }

    private void OnTriggerEnter(Collider p_col)
    {
        if (p_col.CompareTag("PlayerAttack")) PhysicalHit(1);
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
        throw new System.NotImplementedException();
    }

    protected override void Movement()
    {
        throw new System.NotImplementedException();
    }

    
}
