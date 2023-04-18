using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class QuantityModifier : MonoBehaviour
{
    /* Quantities attributes */
    private int[] quantities = new int[] { 1, 10, 100, -1 }; // List of all possible quantities
    private int currentIndex = 0; // Current index of the quantities list

    /* GameObjects */
    private GameManager manager; // Unique GameManager
    private TextMeshProUGUI textBox; // Text used to display the quantity

    // Start is called before the first frame update
    void Start()
    {
        /* Find GameManager GameObject on whole scene since there is only one */
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();

        /* Set quantity attribute of GameManager to the first element of the quantities array */
        manager.quantity = quantities[currentIndex];

        /* Get TextMesh GameObject */
        textBox = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Set text to either "Max" or "x" followed by the current quantity value. */
        textBox.text = currentIndex == quantities.Length - 1 ? "Max" : "x" + quantities[currentIndex].ToString();
    }

    /// The Click function updates the quantity value in the manager object based on the current index and
    /// quantities array.
    /// 
    /// Returns:
    ///   The method is returning nothing (void).
    public void Click()
    {
        currentIndex = (currentIndex + 1) % quantities.Length;
        manager.quantity = quantities[currentIndex];
    }
}
