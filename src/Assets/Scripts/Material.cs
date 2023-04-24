using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItem {
    public string name;
    public int quantity;
    public bool selected;
}

public class Material : MonoBehaviour
{
    public string nameMaterial = "";
    public int quantity = 1;
    public bool selected = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        selected = !selected;

        if (selected)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            return;
        }
        
        this.gameObject.GetComponent<Image>().color = Color.clear;
    }
}
