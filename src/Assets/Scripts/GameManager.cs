using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class UserData
{
    public float gold;

    public static UserData Load()
    {
        FileInfo data = new FileInfo("Assets/Data/User.json");
        string JSONContent = File.ReadAllText(data.FullName);
        return JsonUtility.FromJson<UserData>(JSONContent);
    }

    public static void Save()
    {
        /* Find GameManager GameObject on whole scene since there is only one */
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        GameManager manager = gameObjectFinder.GetComponent<GameManager>();

        UserData tempData = new UserData();
        tempData.gold = manager.gold;
        FileInfo data = new FileInfo("Assets/Data/User.json");
        string json = JsonUtility.ToJson(tempData) + Environment.NewLine;
        File.WriteAllText(data.FullName, json);
    }
}

public class GameManager : MonoBehaviour
{
    /* Prefabs */
    public GameObject potionPrefab;
    public GameObject potionInfoPrefab;
    public GameObject materialPrefab;
    public GameObject materialCraftPrefab;

    /* Player's variables */
    [HideInInspector]
    public float gold; // Total Gold of the player
    public int quantity; // Current quantity modifier - See QuantityModifier.cs for more details

    private GameObject potionContainer;
    private GameObject potionInfoContainer;
    private GameObject materialCraftContainer;
    private GameObject materialHDVContainer;

    /* Parameters */
    public string floatPrecision = "n2"; // Precision of float type used among all scripts

    // Start is called before the first frame update
    void Start()
    {
        gold = UserData.Load().gold;

        potionContainer = FindInActiveObjectByName("PotionContainer");
        potionInfoContainer = FindInActiveObjectByName("PotionInfoContainer");
        materialCraftContainer = FindInActiveObjectByName("MaterialContainer");
        materialHDVContainer = FindInActiveObjectByName("MaterialShop");

        loadPotions();
        loadPotionsInfo();
        loadAllMaterials();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
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

    public void loadPotionsInfo() {
        foreach (Transform child in potionInfoContainer.transform) {
            GameObject.Destroy(child.gameObject);
        }

        DirectoryInfo dir = new DirectoryInfo("Assets/Data/Potions");
        FileInfo[] files = dir.GetFiles("*.json");

        /* For each JSON file in the dir */
        foreach (var file in files) {
            /* Get content of file and create PotionItem */
            string JSONContent = File.ReadAllText(file.FullName);

            PotionItem potion = PotionItem.CreateFromJSON(JSONContent);

                /* Create new potion prefab */
                GameObject potionInfoPrefabInstance = (GameObject)Instantiate(potionInfoPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                potionInfoPrefabInstance.GetComponent<PotionInfo>().potionName = potion.name;
                potionInfoPrefabInstance.GetComponent<PotionInfo>().materials = potion.materials;
                potionInfoPrefabInstance.GetComponent<PotionInfo>().known = potion.known;
                potionInfoPrefabInstance.GetComponent<PotionInfo>().level = potion.level;

                potionInfoPrefabInstance.transform.SetParent(potionInfoContainer.transform);
            }
    }

    public void loadAllMaterials()
    {   
        loadMaterials(materialCraftContainer, materialCraftPrefab);
        loadMaterials(materialHDVContainer, materialPrefab);
    }

    public void loadMaterials(GameObject parent, GameObject prefab) {
        DirectoryInfo dir = new DirectoryInfo("Assets/Data/Materials");
        FileInfo[] files = dir.GetFiles("*.json");

        /* For each JSON file in the dir */
        foreach (var file in files) {
            /* Get content of file and create PotionItem */
            string JSONContent = File.ReadAllText(file.FullName);
            MaterialItem material = MaterialItem.CreateFromJSON(JSONContent);
            
            GameObject materialCraftPrefabInstance = (GameObject)Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            
            materialCraftPrefabInstance.GetComponent<Material>().nameMaterial = material.name;
            materialCraftPrefabInstance.GetComponent<Material>().quantity = material.quantity;
            materialCraftPrefabInstance.GetComponent<Material>().selected = material.selected;
            materialCraftPrefabInstance.GetComponent<Material>().cost = material.cost;
            materialCraftPrefabInstance.GetComponent<Material>().filePath = file.FullName;
            //Debug.Log(prefabInstance);
            //Debug.Log(file);
            materialCraftPrefabInstance.transform.SetParent(parent.transform);
        }
    }
}
