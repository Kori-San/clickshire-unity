using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Balance : MonoBehaviour
{
    private GameManager manager;
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObjectFinder = GameObject.Find("GameManager");
        manager = gameObjectFinder.GetComponent<GameManager>();

        gameObjectFinder = GameObject.Find("Balance");
        textBox = gameObjectFinder.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textBox.text = manager.gold.ToString(manager.floatPrecision) + " Gold";
    }
}
