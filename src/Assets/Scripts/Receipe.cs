using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipe : MonoBehaviour
{
    private GameManager manager; 
    public string filePath;

    public PotionItem tempPotion;

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
