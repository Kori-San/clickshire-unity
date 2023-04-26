using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;
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
        sentences.Clear();
        Debug.Log(dialog.sentences);
        foreach (string sentence in dialog.sentences)
        {
            Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }
        dialogBox.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        Debug.Log("End of dialog");
    }
}
