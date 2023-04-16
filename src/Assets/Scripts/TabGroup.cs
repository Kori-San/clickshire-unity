using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    private void Start() {
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(i == 0);
        }

        return;
    }

    public void OnTabEnter(TabButton button)
    {

    }

    public void OnTabExit(TabButton button)
    {
        
    }
    
    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        int index = button.transform.GetSiblingIndex();
        Debug.Log(index);
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(i == index);
        }
    }
}
