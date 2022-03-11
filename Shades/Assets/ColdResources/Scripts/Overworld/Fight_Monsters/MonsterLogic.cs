using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterData))]
public abstract class MonsterLogic : MonoBehaviour
{
    protected MonsterData _data;
    protected virtual void Init()
    {
        MonsterData _data = GetComponent<MonsterData>();
        _data.InitData();
    }
    protected abstract void Movement();
    protected abstract void Attack(GameObject p_attack, Transform p_target);
    protected virtual void PhysicalHit(int p_depletedHP)
    {
        _data.HP -= p_depletedHP;
        if (_data.HP <= 0)
        {
            Killed();
        }
    }

    protected abstract void SpellHit();
    protected abstract void Killed();
}
