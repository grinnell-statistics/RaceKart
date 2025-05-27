using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Class: ShowValue
 *        - shows the current value of the slider on
 *        - the right side so the user knows what
 *        - value is being chosen in the Sandbox
 */
public class ShowValue : MonoBehaviour
{
    Text percentageText;
  
    void Start()
    {
        percentageText = GetComponent<Text> ();
    }

    // Update is called once per frame
    public void textUpdate (float value)
    {
        //shows the slider value to two decimal points
        percentageText.text = value.ToString("0.00");
    }
}
