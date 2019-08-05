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

        public static Tuple<int, int> AbsDistance(TileObject t1, TileObject t2)
        {
            return new Tuple<int, int>
                (Mathf.Abs(t1.CurrentIndex.Item1 - t2.CurrentIndex.Item1), Mathf.Abs(t1.CurrentIndex.Item2 - t2.CurrentIndex.Item2));
        }
        public static Tuple<int, int> Distance(TileObject t1, TileObject t2)
        {
            return new Tuple<int, int>
                ((t2.CurrentIndex.Item1 - t1.CurrentIndex.Item1), (t2.CurrentIndex.Item2 - t1.CurrentIndex.Item2));
        }

        public static int MaxDistance(TileObject t1, TileObject t2)
        {
            var dis = AbsDistance(t1, t2);
            return dis.Item1 > dis.Item2 ? dis.Item1 : dis.Item2;
        }
    }
}
