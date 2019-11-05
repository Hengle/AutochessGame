using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : PlayerManagerBehavior
{
    private Bank _bank;

    private void Awake()
    {
        _bank = GetComponent<Bank>();   
    }

    public void GetChessBuild()
    {

    }
}
