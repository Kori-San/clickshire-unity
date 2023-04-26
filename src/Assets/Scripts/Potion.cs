using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

[System.Serializable]
public class PotionItem
{
    public string name;
    public float initCost;
    public float initValue;
    public float modifier;
    public int level;
    public string[] materials;
    public bool known;
    public DialogItem dialog;

    public static PotionItem CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PotionItem>(jsonString);
    }

    public static PotionItem CreateFromPotion(Potion potion)
    {
        PotionItem returnedPotion = new PotionItem();

        returnedPotion.name = potion.potionName;
        returnedPotion.initCost = potion.initCost;
        returnedPotion.initValue = potion.initValue;
        returnedPotion.modifier = potion.modifier;
        returnedPotion.level = potion.currentLevel;
        returnedPotion.materials = potion.materials;
        //returnedPotion.dialog = potion.dialog;

        return returnedPotion;
    }

    public void SaveToJSON(string filePath)
    {
        string json = JsonUtility.ToJson(this) + Environment.NewLine;
        File.WriteAllText(filePath, json);
    }
}

public class Potion : MonoBehaviour
{
    /* GameObjects */
    private GameManager manager; // GameManager - Unique to the scene

    private GameObject clickButton; // Click Button of THIS Potion
    private GameObject upgradeButton; // Upgrade Button of THIS Potion
    private GameObject goldValue; // Text informing the value of THIS Potion

    public string filePath; // Path of Data file

    /* Initial Values - Used for calculations of Cost per level and Value per level */
    public string potionName;
    public float initCost; // Cost of level 1
    public float initValue; // Value of level 1
    public float modifier; // Cost modifier used for Cost Calculation
    public string[] materials;
    public Dialog dialog;

    /* Current Value - Update every level */
    private float currentValue; // Value gained when clicking THIS Potion

    /* Levels */
    public int currentLevel; // Current Level of THIS Potion
    private int targetLevel; // Target Level is the level target by the upgrade button

    // Start is called before the first frame update
    void Start()
    {
        /* Find GameManager GameObject on whole scene since there is only one */
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();

        /* Get 'Informations' GameObject of THIS Potion */
        gameObjectFinder = transform.Find("Informations").gameObject;

        /* Store 'ClickButton' of THIS Potion */
        clickButton = transform.Find("ClickButton").gameObject;

        /* Store both 'UpgradeButton' AND 'GoldValue' of THIS Potion */
        upgradeButton = gameObjectFinder.transform.Find("UpgradeButton").gameObject;
        goldValue = gameObjectFinder.transform.Find("GoldValue").gameObject;

        currentValue = CalculateValue(currentLevel);

        return;
    }

    // Update is called once per frame
    void Update()
    {
        /* Set Target Level to either MaxLevel (maximum upgrade possible) or the value of the quantity button */
        targetLevel = manager.quantity == -1 ? FindMaxLevel() : currentLevel + manager.quantity;

        /* Display Informations */
        ChangeChildTextMeshPro(clickButton, "", currentLevel.ToString());
        ChangeChildTextMeshPro(upgradeButton, "LevelCostText", CalculateCost(targetLevel).ToString(manager.floatPrecision) + " Gold");
        ChangeChildTextMeshPro(upgradeButton, "LevelGainText", "x" + (targetLevel - currentLevel));
        ChangeChildTextMeshPro(goldValue, "", currentValue.ToString(manager.floatPrecision) + " Gold");

        return;
    }

    /// This function changes the text value of a TextMeshProUGUI component in a given GameObject or its
    /// child object.
    /// 
    /// Args:
    ///   GameObject: The initial game object that contains the TextMeshProUGUI component that needs to be
    ///               changed.
    ///   label (string): The name of the child object whose TextMeshProUGUI component needs to be changed.
    ///                   If label is an empty string, then the TextMeshProUGUI component of the initialObject's
    ///                   first child will be changed.
    ///   textValue (string): The new text value that will be assigned to the TextMeshProUGUI component.
    /// 
    /// Returns:
    ///   The method is returning nothing (void).
    private void ChangeChildTextMeshPro(GameObject initialObject, string label, string textValue)
    {
        GameObject finalObject = initialObject.transform.Find(label).gameObject;
        TextMeshProUGUI objectText = label == "" ? initialObject.GetComponentInChildren<TextMeshProUGUI>() : finalObject.GetComponent<TextMeshProUGUI>();
        objectText.text = textValue;

        return;
    }

    /// This function calculates the cost of reaching a target level in a game based on a specific formula.
    /// 
    /// Args:
    ///   targetLevel (int): The level that the user wants to calculate the cost for.
    /// 
    /// Returns:
    ///   The method is returning a float value which represents the cost to reach a certain target level in
    ///   a game, based on a formula that takes into account the initial cost, a modifier, the current level,
    ///   and the target level.
    private float CalculateCost(int targetLevel)
    {
        // https://www.kongregate.com/forums/9268-kongregate-published-games/topics/453018-adventure-capitalist-web-version-commonly-requested-formulas
        float targetModifier = (float)Math.Pow(modifier, targetLevel);
        float currentModifier = (float)Math.Pow(modifier, currentLevel);

        float costNumerator = initCost * ((targetModifier) - (currentModifier));
        float costDenominator = (modifier - 1.0f);

        float targetCost = costNumerator / costDenominator;
        return targetCost;
    }

    private float CalculateValue(int targetLevel)
    {
        return targetLevel * initValue;
    }

    /// This function finds the maximum level that can be purchased based on the current level and available
    /// gold.
    /// 
    /// Returns:
    ///   The method is returning an integer value which represents the maximum level that can be purchased
    ///   with the current amount of gold available.
    private int FindMaxLevel()
    {
        int maxLevel = currentLevel + 1;
        float maxCost = CalculateCost(maxLevel);

        while (maxCost < manager.gold && (maxLevel - currentLevel) <= 1000) // Maximum 1000 level diff
        {
            maxLevel++;
            maxCost = CalculateCost(maxLevel);
        }

        return maxLevel == currentLevel + 1 ? maxLevel : maxLevel - 1;
    }

    /// The Click function adds the current value to the gold variable in the manager object.
    ///
    /// Returns:
    ///   The method is returning nothing (void).
    public void Click()
    {
        manager.gold += currentValue;
        UserData.Save();
    }

    /// The Upgrade function updates the current value and level of an object if the player has enough gold
    /// to pay for the upgrade cost.
    /// 
    /// Returns:
    ///   If the manager's gold is less than the current cost, then the function returns without performing
    ///   any further actions.
    public void Upgrade()
    {
        float currentCost = CalculateCost(targetLevel);

        if (manager.gold < currentCost)
        {
            return;
        }

        manager.gold -= currentCost;
        UserData.Save();

        currentValue = CalculateValue(targetLevel);
        currentLevel = targetLevel;

        PotionItem tempPotion = PotionItem.CreateFromPotion(this);
        tempPotion.SaveToJSON(filePath);
    }
}
