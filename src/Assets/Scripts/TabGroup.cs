using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    /* Lists of TabButton and Bodies */
    private List<TabButton> tabButtons; // List containing all the Tab Buttons
    public List<GameObject> objectsToSwap; // List containing of all Bodies linked to the Tabs

    /* Buttons attributes */
    private TabButton selectedTab;

    /// This function adds a TabButton to a list of TabButtons.
    /// 
    /// Args:
    ///   TabButton: TabButton is a class type parameter that represents a button used for tab navigation in
    ///              a user interface. The method "Subscribe" takes an instance of this class as a parameter.
    /// 
    /// 
    /// Returns:
    ///   The method is returning nothing (void).
    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);

        return;
    }

    private void Start()
    {
        /* Add all Tabs to the tabButtons list */
        foreach (Transform child in transform)
        {
            Subscribe(child.gameObject.GetComponent<TabButton>());
        }

        /* Set the default tab to index 0 and not tabButtons[tabButtons.Lenght - 1] by default */
        OnTabSelected(tabButtons[0]);
        return;
    }

    public void OnTabEnter(TabButton button)
    {

    }

    public void OnTabExit(TabButton button)
    {

    }

    /// This function swaps between different objects based on the selected tab button and changes the color
    /// of the tab button accordingly.
    /// 
    /// Args:
    ///   TabButton: TabButton is a custom class or component that represents a button used for tab
    ///              navigation in a user interface. It is likely defined elsewhere in the codebase.
    /// 
    /// Returns:
    ///   The method is returning nothing (void).
    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        int index = button.transform.GetSiblingIndex();

        /* Swaps between different objects based on the selected tab button and changing the color of the tab button accordingly. */
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            objectsToSwap[i].SetActive(i == index);
            tabButtons[i].GetComponent<Image>().color = i == index ? tabButtons[i].GetComponent<TabButton>().baseColor : Color.white ;
        }

        return;
    }
}
