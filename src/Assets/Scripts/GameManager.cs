using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public float gold;
    public string floatPrecision = "n2";
    public int quantity;

    // Start is called before the first frame update
    void Start()
    {
        // Charger la save
        gold = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
