using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class FlowManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float counter = 10;

    public void StartRound()
    {
        var controllers = FindObjectsOfType<TileObjectController>();
        StopAllCoroutines();
        counter = 10;

        foreach (var ctrl in controllers)
        {
            ctrl.EnableForPlay();
        }
    }

    private void Awake()
    {
        text.text = counter.ToString();
        StartCoroutine(DecreaseCounter());
    }

    private IEnumerator DecreaseCounter()
    {
        while (counter > 0)
        {
            counter -= Time.deltaTime;
            text.text = counter.ToString("0");

            yield return null;
        }
        counter = 0;
        StartRound();
    }
}
