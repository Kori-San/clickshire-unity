using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    /* Attributes */
    private TabGroup tabGroup; // Tab Group which is used to manage tabs
    public Color baseColor; // Color of the tab
    public bool isFirstSeen; // Check if it's the first time to display this scene

    // Start is called before the first frame update
    void Start()
    {
        tabGroup = transform.parent.gameObject.GetComponent<TabGroup>();
        isFirstSeen = false;
        return;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}
