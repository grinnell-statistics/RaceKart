using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Class: SpecDisplay
   Purpose: Toggles the display of Specs On/Off in the Garage scene
 */
 
public class SpecDisplay : MonoBehaviour
{
    public GameObject Display;
    public GameObject CarModels;
    public bool SpecDisplayOn;
    public CarChooser CarChooser;

    public GameObject carHint;
    
    void Start()
    {
      SpecDisplayOn = false;
      Debug.Log ("level: " + DataManager.gameData.level);
      if (string.Compare (DataManager.gameData.level, "BumpyTrack") == 0)
      {
        carHint.SetActive(true);
      }
      else
      {
        carHint.SetActive(false);
      }
    }

    public void DisplayClicked(){
        SpecDisplayOn = true;
        Display.SetActive(true);
        CarModels.SetActive(false); 
        CarChooser.Display();
    }

    public void ExitButtonClicked(){
        SpecDisplayOn = false;
        CarChooser.Display();
        Display.SetActive(false);  
        CarModels.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
