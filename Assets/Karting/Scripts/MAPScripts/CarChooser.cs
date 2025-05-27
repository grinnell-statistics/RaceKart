using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.UI;
using UnityEngine.UI;

// This script manages which cars to display in garage scene. 
public class CarChooser : MonoBehaviour
{
    private int x = 0;
    private int y = 0;
    private int engine = 0;
    public GameObject[] carsForArrange;
    public GameObject SpecButton;
    //public SpecDisplayScript SpecDisplay ;
    
    // allCars is a 2D array to be used to organize the wheel and body choice.
    private GameObject[,] allCars = new GameObject[4, 4];
    public GameObject currentDisplay;

    private void Start()
    {
        // Arranges the 2D array
        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                allCars[i, j] = carsForArrange[c];
                c++;
            }
        }

        // Set up which scene to load after garage scene.
        LoadSceneButton button = GameObject.Find("StartButton").GetComponent<LoadSceneButton>();
        button.SceneName = DataManager.gameData.level;
    }

    // The three select functions below accomodate for looping around the end of the array to the beginning.

    // Select Wheel
    public void SelectWheel(bool right)
    { 
        if (right)
        {
            x++;
            if (x > 3)
                x = 0;
        }
        else
        {
            x--;
            if (x < 0)
            {
                x = 3;
            }
        }
        Display();
    }

    // Select Engine
    public void SelectEngine(bool right)
    {
        if (right)
        {
            engine++;
            if (engine > 2)
                engine = 0;
        }
        else
        {
            engine--;
            if (engine < 0)
            {
                engine = 2;
            }
        }
        DataManager.engineChoice = engine;
    }

    // Select Body
    public void SelectBody(bool right)
    {
        if (right)
        {
            y++;
            if (y > 3)
                y = 0;
        }
        else
        {
            y--;
            if (y < 0)
            {
                y = 3;
            }
        }
        Display();
    }

    // Arranges the correct prefab car to display and updates DataManager x, y values that will be used in the SetCar script. 
    public void Display()
    {
        
        //Debug.Log (" Display");

        GameObject displayCar = Instantiate<GameObject>(allCars[y, x], transform.parent);

        if (SpecButton.GetComponent<SpecDisplay>().SpecDisplayOn == true)
        {
            //Debug.Log (" Spec Display: " + SpecButton.GetComponent<SpecDisplay>().SpecDisplayOn);
            displayCar.SetActive(false);
            currentDisplay.SetActive(false);
        }
        else if (SpecButton.GetComponent<SpecDisplay>().SpecDisplayOn == false)
        {   
           
            //Debug.Log (" Spec Display: " + SpecButton.GetComponent<SpecDisplay>().SpecDisplayOn);
            displayCar.name = currentDisplay.name;
            displayCar.transform.position = currentDisplay.transform.position;
            displayCar.transform.rotation = currentDisplay.transform.rotation;
            Destroy(currentDisplay);
            currentDisplay = displayCar;
            DataManager.x = x;
            DataManager.y = y;
            displayCar.SetActive(true);
            currentDisplay.SetActive(true);
        }    
    }

}
