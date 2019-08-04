using System;
using UnityEngine;

namespace GridSystem
{
    [System.Serializable]
    public class TileObject : MonoBehaviour
    {
        public Tuple<int,int> CurrentIndex { get { return tile.locale; } }
        public bool IsOnGrid => CurrentIndex == null ? false : true;
        public void RemoveFromGrid() => tile.Release();
        [HideInInspector]
        public Tile tile;
    }
}
