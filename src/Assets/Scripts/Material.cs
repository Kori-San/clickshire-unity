using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItem {
    public string name;
    public int quantity;
    public bool selected;
    public float cost;

    public static MaterialItem CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<MaterialItem>(jsonString);
    }

    public static MaterialItem CreateFromMaterial(Material material)
    {
        MaterialItem returnedMaterial = new MaterialItem();

        returnedMaterial.name = material.nameMaterial;
        returnedMaterial.quantity = material.quantity;
        returnedMaterial.selected = material.selected;
        returnedMaterial.cost = material.cost;
        
        return returnedMaterial;
    }
}

public class Material : MonoBehaviour
{
    private GameManager manager;
 
    public string nameMaterial = "";
    public int quantity = 1;
    public float cost;
    public bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (quantity < 1)
        {
            return;
        }
        selected = !selected;
        ChangeBackground();
    }

    public void ChangeBackground()
    {
        if (selected)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            return;
        }
        
        this.gameObject.GetComponent<Image>().color = Color.clear;
    }

    public void Buy()
    {
        manager.gold -= cost;
        quantity += 1;
    }
}
