using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Class: WinSceneLoadInfo
 *        - loads info on WinScene (not Leaderboard info)
 */
public class WinSceneLoadInfo : MonoBehaviour
{
    public Text playerID;
    public Text groupID;
    public Text level;
    public Text time;
    public Text penalty;

    // Sets the information of the player in the Win Scene
    void Start()
    {
        playerID.text = DataManager.gameData.playerID;
        groupID.text = DataManager.gameData.groupID;
        level.text = DataManager.gameData.level;
        time.text = DataManager.gameData.time.ToString().Substring(0,5);
        penalty.text = (Booster.count * 1f).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
