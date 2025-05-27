using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.UI;

/* Class: SceneLevelSet
 * Purpose: Reloads the current level after the user presses 'Restart'
 */
public class SceneLevelSet : MonoBehaviour
{
    
    void Start()
    {
        LoadSceneButton button = GameObject.Find("RestartButton").GetComponent<LoadSceneButton>();
        button.SceneName = DataManager.gameData.level;
    }

}