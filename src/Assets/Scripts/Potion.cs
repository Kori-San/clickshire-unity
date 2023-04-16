using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Potion : MonoBehaviour
{
    /* GameObjects */
    private GameManager manager;

    private GameObject clickButton;
    private GameObject upgradeButton;
    private GameObject goldValue;
    
    /* Initial Values - Get them from JSON later */
    private float initCost = 3.738f;
    private float initValue = 1.0f;
    private float modifier = 1.07f;

    /* Current Values - Update for each level */
    private float currentValue;

    /* Levels */
    private int currentLevel = 1;
    private int targetLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();

        clickButton = transform.Find("ClickButton").gameObject;
        upgradeButton = transform.Find("UpgradeButton").gameObject;
        goldValue = transform.Find("GoldValue").gameObject;

        currentValue = initValue;

        // Charger la save
        return;
    }

    // Update is called once per frame
    void Update()
    {
        targetLevel = manager.quantity == -1 ? findMaxLevel() : currentLevel + manager.quantity;

        /* Display Informations */
        TextMeshProUGUI clickButtonText = clickButton.GetComponentInChildren<TextMeshProUGUI>();
        clickButtonText.text = currentLevel.ToString();

        TextMeshProUGUI upgradeButtonText = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        upgradeButtonText.text = calculateCost(targetLevel).ToString(manager.floatPrecision) + " Gold";

        TextMeshProUGUI goldValueText = goldValue.GetComponent<TextMeshProUGUI>();
        goldValueText.text = currentValue.ToString(manager.floatPrecision) + " Gold";

        return;
    }

    private float calculateCost(int targetLevel)
    {
        // https://www.kongregate.com/forums/9268-kongregate-published-games/topics/453018-adventure-capitalist-web-version-commonly-requested-formulas
        float targetModifier = (float)Math.Pow(modifier, targetLevel);
        float currentModifier = (float)Math.Pow(modifier, currentLevel);

        float costNumerator = initCost * ((targetModifier) - (currentModifier));
        float costDenominator = (modifier - 1.0f);

        float targetCost = costNumerator / costDenominator;
        return targetCost; 
    }

    private int findMaxLevel()
    {
        int maxLevel = currentLevel + 1;
        float maxCost = calculateCost(maxLevel);

        if (maxCost >= manager.gold)
        {
            return maxLevel;
        }

        while (maxCost < manager.gold)
        {
            maxLevel++;
            maxCost = calculateCost(maxLevel);
        }

        return maxLevel - 1; 
    }

    public void Click()
    {
        manager.gold += currentValue;
    }

    public void Upgrade()
    {
        float currentCost = calculateCost(targetLevel);

        if (manager.gold < currentCost)
        {
            return; 
        }

        manager.gold -= currentCost;
        currentValue = targetLevel * initValue; 
        currentLevel = targetLevel;
    }
}
