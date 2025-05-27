using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Class: PanelTire
//Purpose: Implememts the Tire Panel left and right button in the Garage Scene
public class PanelTire : MonoBehaviour
{
    // index of current wheel
    public static int currentSprite;

    // arrays of kart models
    public GameObject[] models;

    // array of texts
    public GameObject[] wheelNames;

    // tag for type of tire
    public static string tireTag; 

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = 0;
        tireTag = "classic";
        models[currentSprite].SetActive(true);
    }

    // Function: OnClickChangeBackground
    //  Purpose: Changes image of Tire panel in Garage Scene when right button is clicked
    public void OnClickChangeBackground()
    {
        // set current tire model inactive
        models[currentSprite].SetActive(false);
        wheelNames[currentSprite].SetActive(false);

        // if current tire is classic, change to roadster
        if (currentSprite == 0)
        {
            currentSprite++;
            tireTag = "roadster";
        }

        // if current tire is roadster, change to 4x4
        else if (currentSprite == 1)
        {
            currentSprite++;
            tireTag = "4x4";
        }

        // if current tire is 4x4, change to muscle
        else if (currentSprite == 2)
        {
            currentSprite++;
            tireTag = "muscle";
        }

        // if current tire is muscle, then change to classic
        else if (currentSprite == 3)
        {
            currentSprite = 0;
            tireTag = "classic";
        }

        // set new tire model active
        models[currentSprite].SetActive(true);

        wheelNames[currentSprite].SetActive(true);
    }

    // Function: OnClickReverseChangeBackground
    //  Purpose: Changes image of Tire panel in Garage Scene when left button is clicked
    public void OnClickReverseChangeBackground()
    {
        // set current tire model inactive 
        models[currentSprite].SetActive(false);

        wheelNames[currentSprite].SetActive(false);

        // if current tire is classic, change to muscle
        if (currentSprite == 0)
        {
            currentSprite = 3;
            tireTag = "muscle";
        }

        // if current tire is roadster, change to classic
        else if (currentSprite == 1)
        {
            currentSprite--;
            tireTag = "classic";
        }

        // if current tire is 4x4, change to roadster
        else if (currentSprite == 2)
        {
            currentSprite--;
            tireTag = "roadster";
        }

        // if current tire is muscle, change to 4x4
        else if (currentSprite == 3)
        { 
            currentSprite--;
            tireTag = "4x4";
        }

        // set new tire model active
        models[currentSprite].SetActive(true);
        wheelNames[currentSprite].SetActive(true);
    }

}
