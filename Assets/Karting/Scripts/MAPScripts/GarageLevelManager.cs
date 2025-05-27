using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//   Class: GarageLevelManahger:
// Purpose: Manages the garage scene
public class GarageLevelManager : MonoBehaviour
{

    public GameObject startBtnLevel1;
    public GameObject startBtnLevel2;
    public GameObject startBtnLevel3;
    public GameObject openingPanel;
    public static int prevSceneNum;

    void Start()
    {
        prevSceneNum = Indestructable.prevScene; 
        startBtnLevel1.SetActive(true);
        startBtnLevel2.SetActive(false);
        startBtnLevel3.SetActive(false);

        if (prevSceneNum == 10){
            openingPanel.SetActive(false);
            startBtnLevel1.SetActive(false);
            startBtnLevel2.SetActive(true);
            startBtnLevel3.SetActive(false);
        }

        if (prevSceneNum >14){
            openingPanel.SetActive(false);
            startBtnLevel1.SetActive(false);
            startBtnLevel2.SetActive(false);
            startBtnLevel3.SetActive(true);
            
        }
    }
}
