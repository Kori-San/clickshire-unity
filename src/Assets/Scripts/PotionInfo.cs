using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PotionInfo : MonoBehaviour
{
    public string potionName;
    public string[] materials;
    public int level;
    public bool known;

    private GameObject iconImage;
    private GameObject nameText;
    private GameObject materialsText;

    // Start is called before the first frame update
    void Start()
    {
        iconImage = transform.Find("Icon").gameObject;
        nameText = transform.Find("Name").gameObject;
        materialsText = transform.Find("Materials").gameObject;

        if (level == 0) {
            iconImage.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
        
        if (!known) {
            ChangeChildTextMeshPro(nameText, "", "???");
            ChangeChildTextMeshPro(materialsText, "", "???");
        }
        else {
            ChangeChildTextMeshPro(nameText, "", potionName);
            ChangeChildTextMeshPro(materialsText, "", materials.Length == 0 ? "None" : "Materials:\n" + String.Join("\n", materials));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
