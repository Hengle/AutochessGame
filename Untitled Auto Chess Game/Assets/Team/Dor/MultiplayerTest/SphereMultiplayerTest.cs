using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMultiplayerTest : GuyBehavior
{
    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            networkObject.SendRpc(RPC_MOVE_UP, Receivers.All);
        }
    }

    public override void MoveUp(RpcArgs args)
    {
        transform.position += Vector3.up;
    }
}
