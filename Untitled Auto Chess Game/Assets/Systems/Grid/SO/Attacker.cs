using GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attacker : TileObjectProperties
{
    public int damage = 50;
    [Tooltip("Attacks Per Second")]
    public float attackRate = 0.75f;
    [Tooltip("Radius In Tiles")]
    public int range = 1;

    public UnityEvent AttackBegin = new UnityEvent();
    public UnityEvent AttackEnd = new UnityEvent();

    private bool _canAtk = true;

    public IEnumerator Attack(Attackable target)
    {
        // if already attacking or enemy out of range do not allow to attack
        if (!_canAtk || TileObject.MaxDistance(this.tileObj, target.tileObj) > range )
            yield break;

        OnAttackBegin();

        target.ApplyDamage(damage);

        yield return new WaitForSeconds(attackRate);

        OnAttackEnd();
    }

    private void OnAttackBegin()
    {
        _canAtk = false;
        AttackBegin?.Invoke();
    }
    private void OnAttackEnd()
    {
        _canAtk = true;
        AttackEnd?.Invoke();
    }
}
