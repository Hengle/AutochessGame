using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{

    public Item Item;
    public Image Image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if (Image == null)
        {
            Image = GetComponent<Image>();
        }
    }
}
