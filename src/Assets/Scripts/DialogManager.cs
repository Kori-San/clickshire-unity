using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log("Starting conversation with " + dialog.name);
    }
}
