using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeMultiplayerTest : MoveCubeBehavior
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // client code
        if (!networkObject.IsServer)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }


        // server code
        transform.position += new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0
            ) * Time.deltaTime * 5.0f;

        transform.Rotate(Vector3.up, Time.deltaTime * 90);

        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;
    }
}
