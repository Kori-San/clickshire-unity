using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Receipe : MonoBehaviour
{
    private GameManager manager; 
    public string filePath;

    public PotionItem tempPotion;

    private GameObject receipeName;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();
        GameObject info = transform.Find("Informations")?.gameObject;
        receipeName = info?.transform.Find("Name")?.gameObject;
        GameObject buy = info?.transform.Find("BuyButton")?.gameObject;
        GameObject costObject = buy?.transform.Find("CostText")?.gameObject;

        
        TextMeshProUGUI objectCostText = costObject?.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI objectText = receipeName?.GetComponent<TextMeshProUGUI>();
        objectText.text = tempPotion.name;
        objectCostText.text = (tempPotion.initCost * 1.5f).ToString(manager.floatPrecision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyRecipe()
    {
        float cost = tempPotion.initCost * 1.5f;

        if (manager.gold < cost)
        {
            return;
        }

        manager.gold -= cost;
        tempPotion.known = true;

        //PotionItem tempPotion = PotionItem.CreateFromPotion(potion);
        tempPotion.SaveToJSON(filePath);
        
        manager.loadRecipes();
        manager.loadPotionsInfo();
    }
}
