using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
