using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridSystem;

public class PurchaseRobot : MonoBehaviour
{
    public GameObject model;
    public LayerMask layers;
    public GridSystem.Grid grid;

    private Button _btn;
    private GameObject _currentHeld;
    private Tile _currentTile;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    public void MouseDown()
    {
        // Attach robot model to mouse, if purchase available
        // Deduct gold from player

        //print("MouseDown");
        _currentHeld = Instantiate(model);

        StartCoroutine(AttachModelToGrid());
    }

    private IEnumerator AttachModelToGrid()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        while (true)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, layers))
            {
                _currentHeld.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                _currentTile = hit.transform.GetComponent<Tile>();
            }
            else
            {
                _currentHeld.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            }

            yield return null;
        }
    }

    public void MouseUp()
    {
        StopAllCoroutines();

        if (_currentTile != null && grid.InsertToBoard(_currentHeld, _currentTile.locale.Item1, _currentTile.locale.Item2))
        {
            _currentTile = null;
            _currentHeld = null;
        }
        else
        {
            Destroy(_currentHeld);
            _currentHeld = null;
        }

        //print("MouseUp");
    }
}
