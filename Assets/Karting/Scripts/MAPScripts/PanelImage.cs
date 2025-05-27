using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//   Class: PanelImage
// Purpose: Change the image of the engine panel when the right/left button is clicked
public class PanelImage : MonoBehaviour
{
    // index of current model
    public static int currentSprite = 0;

    public string resourceName = "Backgrounds";

    // panel gameobject
    public GameObject panel;

    // array of background images
    public Sprite[] backgrounds;

    // array of engine names
    public GameObject[] engineNames;
 
    void Awake()
    {
        // if (resourceName != "")
        //     backgrounds = Resources.LoadAll<Sprite> (resourceName);
    }

    private void Start()
    {
        currentSprite = 0;
    }

    // Function: OnClickChangeBackground
    //  Purpose: Changes background of engine panel when right arrow is clicked 
    public void OnClickChangeBackground()
    {
        engineNames[currentSprite].SetActive(false);

        // if current sprite is engine 1, change to engine 2
        if (currentSprite == 0)
        {
            currentSprite++;
        }

        // if current sprite is engine 2, change to engine 3
        else if (currentSprite == 1)
        {
            currentSprite++;
        }

        // if current sprite is engine 3, change to engine 1
        else if (currentSprite == 2)
        {
            currentSprite = 0;
            
        }

        GameObject.Find(panel.name).GetComponent<Image>().sprite = backgrounds[currentSprite];
        engineNames[currentSprite].SetActive(true);
    }


    // Function: OnClickReverseChangeBackground
    //  Purpose: Changes background of engine panel when left arrow is clicked 
    public void OnClickReverseChangeBackground()
    {
        engineNames[currentSprite].SetActive(false);

        // if current sprite is engine 1, change to engine 3
        if (currentSprite == 0)
        {
            currentSprite = 2;
        }

        // if current sprite is engine 2, change to engine 1
        else if (currentSprite == 1)
        {
            currentSprite = 0;
        }

        // if current sprite is engine 3, change to engine 2
        else if (currentSprite == 2)
        {
            currentSprite--;
        }

        GameObject.Find(panel.name).GetComponent<Image>().sprite = backgrounds[currentSprite];
        engineNames[currentSprite].SetActive(true);
    }
}
