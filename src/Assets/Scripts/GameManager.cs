using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Prefabs */
    public GameObject potionPrefab;

    /* Player's variables */
    [HideInInspector]
    public float gold; // Total Gold of the player
    public int quantity; // Current quantity modifier - See QuantityModifier.cs for more details

    private GameObject potionContainer;

    /* Parameters */
    public string floatPrecision = "n2"; // Precision of float type used among all scripts

    // Start is called before the first frame update
    void Start()
    {
        // [TEMP] Set gold to 0.
        gold = 0.0f;
        potionContainer = GameObject.Find("PotionContainer");

        loadPotions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadPotions() {
        foreach (Transform child in potionContainer.transform) {
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

                potionPrefabInstance.transform.SetParent(potionContainer.transform);
            }
        }
    }
}
