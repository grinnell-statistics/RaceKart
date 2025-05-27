using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Class: AlterBooster
   Purpose: Used to alter the values shown in the Challenge Mode screen.
*/
public class AlterValuesBooster : MonoBehaviour
{

    //public int ParameterNum;
    public Text Parameter;
    public float MinNumber;
    public float MaxNumber;
    public string ParameterName;
    public Booster booster;
    public GameObject TwoCharacteristicsLimit;
    public GameObject MaxValueWarning;
    public GameObject MinValueWarning;
    private float delaytime = 1.5f;

    [HideInInspector]
    public float increaseTwenty;
    [HideInInspector]
    public float decreaseTwenty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Order of the parameters
    public Text TopSpeed;
    public Text Acceleration;
    public Text AccelerationCurve;
    public Text Suspension;
    public Text CoastingDrag;
    public Text Steer;
    public Text Grip;
    */

    //procedure to increase the value of the characteristic by 20%
    public void IncreaseValue()
    {
        
        //if the range for a characteristic isn't from 0 to 1, then 
        // round up the value to a whole number
        if (MaxNumber!= 1)
        {
            increaseTwenty = Mathf.Ceil((float.Parse(Parameter.text))*1.2f);
        }
        else 
        {
            increaseTwenty = (float.Parse(Parameter.text))*1.2f;
        }
        
        //prevents the user from updating more than two characteristics of a car
        if (Booster.count == 2)
        {
            StartCoroutine (TwoCharacteristicsLimitActive());
        }
        // don't update the number shown if the updated number would be more
        // than the max value, or if the number is already at max value
        else if ((float.Parse(Parameter.text) == MaxNumber) || (increaseTwenty > MaxNumber))
        {
            StartCoroutine (MaxValueWarningActive());
        }
        //if all the conditions are fine, increase the value and then send the 
        //information to an array that records the characateristic changes and 
        // the values changed
        else
        {
            Booster.changedValue[Booster.count] = increaseTwenty;
            Booster.parameterName[Booster.count] = ParameterName;

            Parameter.text = increaseTwenty.ToString();
            //change the colour of the button to indicate change
            this.GetComponent<Image>().color = new Color32(235, 255, 225, 245);
            Booster.count++;
        }
        
    }

    // procedure to decrease the value of a characteristic by 20%
    public void DecreaseValue()
    {
        
        if (MaxNumber != 1 )
        {
             decreaseTwenty = Mathf.Ceil((float.Parse(Parameter.text))*0.8f);
        }
        else 
        {
             decreaseTwenty = (float.Parse(Parameter.text))*0.8f;
        }
       
        
        if (Booster.count == 2)
        {
            StartCoroutine (TwoCharacteristicsLimitActive());
        }
        else if ((float.Parse(Parameter.text) == MinNumber) || (decreaseTwenty<MinNumber))
        {
            StartCoroutine (MinValueWarningActive());
            
        }
        else
        {
            Booster.changedValue[Booster.count] = decreaseTwenty;
            Booster.parameterName[Booster.count] = ParameterName;

            this.GetComponent<Image>().color = new Color32(235, 255, 225, 245);
            Parameter.text = decreaseTwenty.ToString();
            Booster.count++;
        }
        
    }

    // resets the ChallengeMode scene and the values
    public void Reset()
    {
        Debug.Log("Booster Reset");
        Booster.count = 0;
    }

    public void BackButton()
    {
        Booster.count = 0;
    }

    // warning for if the value is already the minimum
    IEnumerator MinValueWarningActive()
    {
        MinValueWarning.SetActive(true);
        yield return new WaitForSeconds (delaytime);
        MinValueWarning.SetActive(false);
    }


    // warning for if the value is already the maximum
    IEnumerator MaxValueWarningActive()
    {
        MaxValueWarning.SetActive(true);
        yield return new WaitForSeconds (delaytime);
        MaxValueWarning.SetActive(false);

    }


    // warning for if the user has already updated 
    // two characteristics and is trying to updated a third one
    IEnumerator TwoCharacteristicsLimitActive()
    {
        TwoCharacteristicsLimit.SetActive(true);
        yield return new WaitForSeconds (delaytime);
        TwoCharacteristicsLimit.SetActive(false);

    }

}
