using System.IO;

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

         DirectoryInfo dir = new DirectoryInfo("Assets/Data/Potions");
         FileInfo[] files = dir.GetFiles("*.json");

        foreach (var file in files) {
            string JSONContent = File.ReadAllText(file.FullName);
            PotionItem potion = PotionItem.CreateFromJSON(JSONContent);
            Debug.Log(potion.name);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
