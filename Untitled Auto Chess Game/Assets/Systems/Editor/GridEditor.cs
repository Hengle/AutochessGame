using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GridSystem;

[CustomEditor(typeof(GridSystem.Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var board = target as GridSystem.Grid;

        var style = new GUIStyle();
        style.fontSize = 12;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;

        GUILayout.Space(12);
        GUILayout.Label("Grid Generation", style);
        GUILayout.Space(8);
        if (GUILayout.Button("Delete Grid"))
        {
            while (board.transform.childCount != 0)
            {
                DestroyImmediate(board.transform.GetChild(0).gameObject);
            }
        }

        if (GUILayout.Button("Create Grid (Override)"))
        {
            while (board.transform.childCount != 0)
            {
                DestroyImmediate(board.transform.GetChild(0).gameObject);
            }

            for (int i = 0; i < board.gridSizeX; i++)
            {
                for (int j = 0; j < board.gridSizeY; j++)
                {
                    Instantiate(board.tile, new Vector3(i, 0, j) * board.scale, Quaternion.identity, board.transform).name = $"Tile_{i}_{j}";
                }
            }
        }
    }
}
