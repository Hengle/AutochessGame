using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attackable : TileObjectProperties
{
    public int maxHP = 500;
    public int CurrentHP { private set; get; }
    public UnityEvent ReceiveDamage = new UnityEvent();
    public UnityEvent Death = new UnityEvent();

    public void ApplyDamage(int damage)
    {
        ReceiveDamage?.Invoke();
        CurrentHP -= damage;

        if (CurrentHP <= 0)
            OnDeath();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        CurrentHP = maxHP;
    }

    private void OnDeath()
    {
        Death.Invoke();
    }
}
