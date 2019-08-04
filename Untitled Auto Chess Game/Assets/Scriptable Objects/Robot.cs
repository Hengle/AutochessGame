using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Robot : ScriptableObject
{
    [SerializeField]
    private Module module;
    
    [SerializeField]
    Socket[] _sockets;
}
