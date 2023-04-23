using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;


public class Craft : MonoBehaviour
{
    private GameObject[] materials;
    
    List<Material> materialCraft = new List<Material>();

    List<PotionItem> potions = new List<PotionItem>();
    // Start is called before the first frame update
    void Start()
    {
        materials =  GameObject.FindGameObjectsWithTag("material");

        DirectoryInfo dir = new DirectoryInfo("Assets/Data/Potions");
        FileInfo[] files = dir.GetFiles("*.json");

        foreach (var file in files) {
            string JSONContent = File.ReadAllText(file.FullName);
            potions.Add(PotionItem.CreateFromJSON(JSONContent));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject materialGameObject in materials)
        {
            Material material = materialGameObject.GetComponent<Material>();

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
        List<string> materialNameCraft = materialCraft.Select(item => item.name).ToList();
        foreach (var potion in potions)
        {
            if (materialNameCraft.Intersect(potion.materials.ToList()).Count() == materialNameCraft.Count())
            {
                Debug.Log(true);
                // TODO Write in json file : set level to 1
            }
        }
    } 
}
