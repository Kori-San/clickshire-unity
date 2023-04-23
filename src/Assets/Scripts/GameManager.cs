using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class PotionItem
{
    public string name;
    public float initCost;
    public float initValue;
    public float modifier;
    public int level;
    public string[] materials;

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

        return returnedPotion;
    }

    public void SaveToJSON(string filePath)
    {
        string json = JsonUtility.ToJson(this) + Environment.NewLine;
        File.WriteAllText(filePath, json);
    }
}

public class GameManager : MonoBehaviour
{
    /* Prefabs */
    public GameObject potionPrefab;

    /* Player's variables */
    [HideInInspector]
    public float gold; // Total Gold of the player
    public int quantity; // Current quantity modifier - See QuantityModifier.cs for more details

    /* Parameters */
    public string floatPrecision = "n2"; // Precision of float type used among all scripts

    // Start is called before the first frame update
    void Start()
    {
        // [TODO] Either load save or init save
        // [TODO] Generate Potions and other tabs

        // [TEMP] Set gold to 0.
        gold = 0.0f;
        loadPotions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadPotions() {
        GameObject container = GameObject.Find("PotionContainer");

        foreach (Transform child in container.transform) {
            GameObject.Destroy(child.gameObject);
        }

        DirectoryInfo dir = new DirectoryInfo("Assets/Data/Potions");
        FileInfo[] files = dir.GetFiles("*.json");

        /* For each JSON file in the dir */
        foreach (var file in files) {
            /* Get content of file and create PotionItem */
            string JSONContent = File.ReadAllText(file.FullName);
            PotionItem potion = PotionItem.CreateFromJSON(JSONContent);

            /* If Potion is unlocked */
            if (potion.level > 0) {
                /* Create new potion prefab */
                GameObject potionPrefabInstance = (GameObject)Instantiate(potionPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                potionPrefabInstance.GetComponent<Potion>().potionName = potion.name;
                potionPrefabInstance.GetComponent<Potion>().initCost = potion.initCost;
                potionPrefabInstance.GetComponent<Potion>().initValue = potion.initValue;
                potionPrefabInstance.GetComponent<Potion>().modifier = potion.modifier;
                potionPrefabInstance.GetComponent<Potion>().currentLevel = potion.level;
                potionPrefabInstance.GetComponent<Potion>().materials = potion.materials;
                potionPrefabInstance.GetComponent<Potion>().filePath = file.FullName;

                potionPrefabInstance.transform.SetParent(container.transform);
            }
        }
    }
}
