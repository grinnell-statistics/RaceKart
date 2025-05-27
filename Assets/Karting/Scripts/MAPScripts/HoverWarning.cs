using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* 
 * Class: HoverWarnings
 * Purpose: Sets active warning/info associated with:
 *          - hovering over info icon in additional variables
 */
public class HoverWarning : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject hoverWarning;
    // Start is called before the first frame update
    void Start()
    {
        hoverWarning.SetActive(false);
    }

    public void Potato()
    {
       
    }
    // hovering over the warning
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverWarning.SetActive(true);
    }

    // mouse is not over the 'warning'
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverWarning.SetActive(false);
    }
}
