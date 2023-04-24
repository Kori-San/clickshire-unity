using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

    public void SaveToJSON(string filePath)
    {
        string json = JsonUtility.ToJson(this) + Environment.NewLine;
        File.WriteAllText(filePath, json);
    }
}

public class Material : MonoBehaviour
{
    private GameManager manager;

    public string filePath;
 
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
        selected = !selected;

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

        MaterialItem tempMaterial = MaterialItem.CreateFromMaterial(this);
        tempMaterial.SaveToJSON(filePath);
    }
}
