using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class QuantityModifier : MonoBehaviour
{
    private int[] quantities = new int[] { 1, 10, 100, -1 };
    private int currentIndex = 0;

    private GameManager manager;
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        //
        textBox = transform.GetComponentInChildren<TextMeshProUGUI>();

        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();
        manager.quantity = quantities[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = currentIndex == quantities.Length - 1 ? "Max" : "x" + quantities[currentIndex].ToString();
    }

    public void Click()
    {
        currentIndex = (currentIndex + 1) % quantities.Length;

        manager.quantity = quantities[currentIndex];
    }
}
