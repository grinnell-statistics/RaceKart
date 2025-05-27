using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LevelID script sets all the correct variables in DataManager according to level selected.
public class LevelID : MonoBehaviour
{

    // These gameobjects are locks and medals. Used for game progress purposes. 
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject medalHonor;
    public GameObject medalGlow;

    private void Start()
    {
        // Unlock levels
        if (lock1 != null)
        {
            switch (DataManager.levelUnlocked)
            {
                case 1:
                    lock1.SetActive(false);
                    break;
                case 2:
                    lock1.SetActive(false);
                    lock2.SetActive(false);
                    break;
                case 3:
                    lock1.SetActive(false);
                    lock2.SetActive(false);
                    lock3.SetActive(false);
                    break;
            }

            // Display medal
            if (DataManager.medalHonor == true)
            {
                medalHonor.SetActive(true);
                medalGlow.SetActive(true);
            }
            else
            {
                medalHonor.SetActive(false);
                medalGlow.SetActive(false);
            }
        }
        DataManager.ResetValues();
    }

    // sets track name
    public void SetLevel(string track)
    {
        if (track != null)
            DataManager.gameData.level = track;
    }

    // sets level 
    public void SetLevelID(string level)
    {
        DataManager.gameData.levelID = level;
    }

    // sets if it has obstacles or not
    public void SetObstacles(bool obstacles)
    {
         if (DataManager.gameData.level.Contains("B"))
        {
            DataManager.gameData.obstacles = 8;
        }
        else if (DataManager.gameData.level.Contains("W") || (DataManager.gameData.level.Contains("S"))
                || (DataManager.gameData.level.Contains("C")) )
        {
            DataManager.gameData.obstacles = 0;
        }
        else
        {
            DataManager.gameData.obstacles = 14;
        }
        //DataManager.gameData.osbtacles = obstacles;
    }

    // sets the surface type
    public void SetSurface(string surface)
    {
        if (surface != null)
            DataManager.gameData.surface = surface;
    }

    // sets curve angle from beginning to checkpoint1
    public void SetCurve1(int curve1)
    {
        DataManager.gameData.curve1 = curve1;
    }

    // sets curve angle from checkpoint1 to checkpoint2
    public void SetCurve2(int curve2)
    {
        if (DataManager.gameData.level.Contains("B"))
        {
            DataManager.gameData.curve2 = 900;
        }
        else
        {
            DataManager.gameData.curve2 = curve2;
        }
    }

    // sets curve angle from checkpoint2 to checkpoint3
    public void SetCurve3(int curve3)
    {
        if (DataManager.gameData.level.Contains("B"))
        {
            DataManager.gameData.curve3 = 630;
        }
        else
        {   
            DataManager.gameData.curve3 = curve3;
        }
    }
}
