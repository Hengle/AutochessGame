using GridSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(TileObject))]
public abstract class TileObjectProperties : MonoBehaviour
{
    public TileObject tileObj { private set; get; }

    private void Awake() => tileObj = GetComponent<TileObject>();
}
