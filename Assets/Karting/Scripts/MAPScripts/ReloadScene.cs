using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*   Class: ReloadScene
     Purpose: Reload the previous scene 
 */
public class ReloadScene : MonoBehaviour
{
    public void ReloadCurrentScene ()
    {
        // if the player resets the game while they are playing the game,
        // send the data to the database
        if (string.Compare(SceneManager.GetActiveScene().name, "Booster") != 0)
        {   
            GameFlowManager.gameResetBool = true;
            DataManager.gameData.restart = TimeManager.timeTaken;
            DataManager.gameData.time = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex ) ;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex ) ;
        }
      
    }
}
