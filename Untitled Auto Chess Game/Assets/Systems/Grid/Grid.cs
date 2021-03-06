﻿using UnityEngine;

namespace GridSystem
{
    public class Grid : MonoBehaviour
    {
        public int gridSizeX = 10;
        public int gridSizeY = 10;
        public GameObject tile;
        public GameObject hero;
        public float scale = 1;

        public float xOffset = 0.5f;
        public float yOffset = 0.5f;

        private Tile[,] _grid;


#if UNITY_EDITOR
        [Header("DEBUG")]

        public bool createHeroes = false;
        public int amount = 0;
        private static int heroCount = 0;

#endif

        private void Awake()
        {
            _grid = new Tile[gridSizeX, gridSizeY];

            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    _grid[i, j] = transform.GetChild(i * gridSizeX + j).GetComponent<Tile>();
                    _grid[i, j].Initialize(this, new System.Tuple<int, int>(i, j));
                }
            }
            #region TEST
#if UNITY_EDITOR

            if (createHeroes)
            {
                for (int i = 0; i < amount; i++)
                {
                    CreateHero(Random.Range(0, gridSizeX), Random.Range(0, gridSizeY));
                }
            }

#endif

            #endregion
        }

        public bool GetTileAvailability(int iX, int iY)
        {
            return IndexInRange(iX, iY) && _grid.GetValue(iX, iY) == null;
        }

        public Tile GetTile(int iX, int iY)
        {
            if (!IndexInRange(iX, iY))
                return null;

            return _grid.GetValue(iX, iY) as Tile;
        }

        public bool InsertToBoard(GameObject obj, int x, int y)
        {
            var to = obj.GetComponent<TileObject>();

            if (GetTileAvailability(x, y))
                return false;

            obj.transform.position = new Vector3(x, 0, y);
            var tile = GetTile(x, y);

            if (tile != null)
            {
                if (!to)
                    return tile.RequestOccupy(obj.AddComponent<TileObject>());
                else
                    return tile.RequestOccupy(to);
            }

            return false;
        }

        //private Tile CreateTile(int x, int y)
        //{
        //    var t = Instantiate(tile, new Vector3(x, 0f, y) * scale, Quaternion.identity, transform).GetComponent<Tile>();
        //    t.name = $"Tile_{x + 1}_{y + 1}";
        //    return t;
        //}

        private bool IndexInRange(int x, int y)
        {
            return x >= 0 && x < gridSizeX && y < gridSizeY && y >= 0;
        }
#if UNITY_EDITOR
        private void CreateHero(int x, int y)
        {
            var t = _grid.GetValue(x, y) as Tile;

            if (t != null)
            {
                if (!t.IsOccupied)
                {
                    var h = Instantiate(hero, new Vector3(x, 0, y), Quaternion.identity);
                    h.name = $"Hero_{heroCount++}";
                    InsertToBoard(h, x, y);
                }
            }
        }
#endif
    }
}

