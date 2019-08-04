using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Tile : MonoBehaviour
    {
        private Grid _grid;
        private TileObject _occupator = null;
        public Tuple<int, int> locale;

        private void Start()
        {
            if (_grid == null)
                Destroy(this.gameObject);
        }

        public void Release()
        {
            _occupator.tile = null;
            _occupator = null;
        }
        public bool RequestOccupy(TileObject g)
        {
            if (!_occupator)
            {
                if (g.tile != null)
                    g.tile._occupator = null;

                _occupator = g;
                g.tile = this;
                return true;
            }
            return false;
        }
        public bool IsOccupied => _occupator == null ? false : true;
        public int MaxDistance(TileObject tileObj)
        {
            return Mathf.Abs(tileObj.CurrentIndex.Item1 - locale.Item1) > Mathf.Abs(tileObj.CurrentIndex.Item2 - locale.Item2) ?
                Mathf.Abs(tileObj.CurrentIndex.Item1 - locale.Item1) : Mathf.Abs(tileObj.CurrentIndex.Item2 - locale.Item2);
        }
        public Tuple<int,int> Distance(TileObject tileObj)
        {
            return new Tuple<int, int>
                (Mathf.Abs(tileObj.CurrentIndex.Item1 - locale.Item1), Mathf.Abs(tileObj.CurrentIndex.Item2 - locale.Item2));
        }

        public void Initialize(Grid grid, Tuple<int, int> position)
        {
            _grid = grid;
            locale = position;
        }

        public Tile GetAdjacent(DirectionEnum direction)
        {
            return _grid.GetTile(this.locale.Item1 + Direction.DirectionAsOffset(direction).Item1, this.locale.Item2 + Direction.DirectionAsOffset(direction).Item2);
        }
    } 
}
