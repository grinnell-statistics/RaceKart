using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//   Class: ModelPanel
// Purpose: Implememts the Model Panel left and right button in the Garage Scene
public class ModelPanel : MonoBehaviour
{
    // index of current model
    public static int currentSprite;

    // array of kart models
    public GameObject[] models;

    // array of texts
    public GameObject[] modelNames;

    // index of previous scene
    public static int prevSceneNum;

    // tag for type of model
    public static string modelTag;

    // index of current scene
    int currentSceneNum;

    void Start()
    {
        prevSceneNum = Indestructable.prevScene;
        currentSprite = 0;
        modelTag = "classic";
        models[currentSprite].SetActive(true);
        currentSceneNum = SceneManager.GetActiveScene().buildIndex;
    }


    // Function: OnClickChangeBackground
    //  Purpose: Changes image of Tire panel in Garage Scene when right button is clicked
    public void OnClickChangeBackground()
    {
        //set current kart model inactive
        models[currentSprite].SetActive(false);
        modelNames[currentSprite].SetActive(false);

        // if current model is classic, change to roadster
        if (currentSprite == 0)
        {
            currentSprite++;
            modelTag = "roadster";
        }

        // if current model is roadster, change to 4x4
        else if (currentSprite == 1)
        {
            currentSprite++;
            modelTag = "4x4";
        }

        // if current model is 4x4, change to muscle
        else if (currentSprite == 2)
        {
            currentSprite++;
            modelTag = "muscle";
        }

        // if current model is muscle, change to classic
        else if (currentSprite == 3)
        {
            currentSprite = 0;
            modelTag = "classic";
        }

        // set new model active 
        models[currentSprite].SetActive(true);
        modelNames[currentSprite].SetActive(true);
    }

    // Function: OnClickReverseChangeBackground
    // Purpose: Changes image of Tire panel in Garage Scene when left button is clicked
    public void OnClickReverseChangeBackground()
    {
        // set current model inactive
        models[currentSprite].SetActive(false);
        modelNames[currentSprite].SetActive(false);

        // if current model in classic, change to muscle
        if (currentSprite == 0)
        {
            currentSprite = 3;
            modelTag = "muscle";
        }

        // if current model is roadster, change to classic
        else if (currentSprite == 1)
        {
            currentSprite--;
            modelTag = "classic";
        }

        // if current model is 4x4, change to roadster
        else if (currentSprite == 2)
        {
            currentSprite--;
            modelTag = "roadster";
        }

        // if current model is muscle, change to classic
        else if (currentSprite == 3)
        {
            currentSprite--;
            modelTag = "4x4";
        }

        // set new model active
        models[currentSprite].SetActive(true);
        modelNames[currentSprite].SetActive(true);
    }
   
}
