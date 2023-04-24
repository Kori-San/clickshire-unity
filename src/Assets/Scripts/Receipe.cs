using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipe : MonoBehaviour
{
    private GameManager manager; 
    public string filePath;

    public Potion potion;

    
    public float initCost;
    public bool known;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyRecipe()
    {
        float cost = initCost * 1.5f;

        if (manager.gold < cost)
        {
            return;
        }

        manager.gold -= cost;
        known = true;

        PotionItem tempPotion = PotionItem.CreateFromPotion(potion);
        tempPotion.SaveToJSON(filePath);
    }
}
