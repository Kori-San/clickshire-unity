using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;


public class Craft : MonoBehaviour
{
    private GameObject[] materials;
    private GameManager manager;
    
    List<Material> materialCraft = new List<Material>();

    List<PotionItem> potions = new List<PotionItem>();
    // Start is called before the first frame update
    void Start()
    {
        materials =  GameObject.FindGameObjectsWithTag("material");
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        materials =  GameObject.FindGameObjectsWithTag("material");
        foreach (GameObject materialGameObject in materials)
        {
            Material material = materialGameObject.GetComponent<Material>();
            if (material.quantity == 0)
            {
                material.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
            }
            if (material.selected && !materialCraft.Find(x => x == material))
            {
                materialCraft.Add(material);
            }
            if (!material.selected && materialCraft.Find(x => x == material))
            {
                materialCraft.Remove(material);
            }
        }
    }

    public void CraftPotion()
    {
        if (materialCraft.Count == 0)
        {
            return;
        }
        List<string> materialNameCraft = materialCraft.Select(item => item.nameMaterial).ToList();
        
        DirectoryInfo dir = new DirectoryInfo("Assets/Data/Potions");
        FileInfo[] files = dir.GetFiles("*.json");   

        DirectoryInfo materialDir = new DirectoryInfo("Assets/Data/Materials");
        FileInfo[] materialFiles = materialDir.GetFiles("*.json");
        
        foreach (var material in materialCraft)
        {
            material.quantity--;
            material.selected = false;
            material.ChangeBackground();
            foreach(var materialFile in materialFiles)
            {
                string JSONContent = File.ReadAllText(materialFile.FullName);
                MaterialItem materialItem = MaterialItem.CreateFromJSON(JSONContent);
                if (materialItem.name == material.nameMaterial)
                {
                    materialItem.quantity = material.quantity;
                    materialItem.SaveToJSON(materialFile.FullName);
                }
            }

        }
        foreach (var file in files) {
            string JSONContent = File.ReadAllText(file.FullName);
            PotionItem potion = PotionItem.CreateFromJSON(JSONContent);
            if (potion.level == 0 && potion.materials.ToList().Intersect(materialNameCraft).Count() == potion.materials.ToList().Count())
            {
                potion.level = 1;
                potion.SaveToJSON(file.FullName);
                manager.loadPotions();
                break;
            }
        }
    } 
}
