using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterLogic : MonoBehaviour
{
    protected MonsterData _data;
    protected virtual void Init()
    {
        _data = GetComponent<MonsterData>();
        _data.InitData();
    }

    protected virtual void OnTriggerEnter2D(Collider2D p_col)
    {
        if (p_col.CompareTag("PlayerDamage"))
        {
            // PhysicalHit(1);
        }
    }

    protected abstract void Movement();
    protected abstract void Attack(GameObject p_attack, Transform p_target);
    protected virtual void PhysicalHit(int p_depletedHP)
    {

#if UNITY_EDITOR
        string _debugString = gameObject.name + " was hit for " + p_depletedHP + " HP";
        if (_data.HP - p_depletedHP <= 0) _debugString += "<color=red> AND WAS KILLED.</color>";
        else _debugString += ". It now sits at " + (_data.HP - p_depletedHP) + " HP.";

        Debug.Log(_debugString);
#endif

        _data.HP -= p_depletedHP;
        if (_data.HP <= 0)
        {
            Killed();
        }


    }

    protected abstract void SpellHit();
    protected abstract void Killed();
}
