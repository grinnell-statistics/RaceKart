using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Class: Leaderboard
 *       - display data in the win scene
 */
public class Leaderboard : MonoBehaviour
{
    public int rowNum;
    public Text PlayerIDText;
    public Text GroupIDText;
    public Text ScoreText;

    private static string[] splitData;


    private static bool isReturned;
    private static bool notUpdated;
    // Start is called before the first frame update
    void Start()
    {
        notUpdated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReturned)
        {
            PlayerIDText.text = "No Data";
            GroupIDText.text = "No Data";
            ScoreText.text = "No Data";
            if (3 * rowNum < splitData.Count() - 1)
            {
                //gets Player ID
                PlayerIDText.text = splitData[3 * rowNum];
            }
            if (3 * rowNum + 1 < splitData.Count())
            {
                //gets Group ID
                GroupIDText.text = splitData[3 * rowNum + 1];
            }
            if (3 * rowNum + 2 < splitData.Count())
            {
                //gets time of completion of track
                ScoreText.text = splitData[3 * rowNum + 2].Split(new char[] { '.' }, System.StringSplitOptions.None)[0] + "." + (splitData[3 * rowNum + 2].Split(new char[] { '.' }, System.StringSplitOptions.None)[1]).Substring(0,2);
            }
            if (notUpdated)
            {
                if (splitData[0] == DataManager.gameData.playerID && splitData[1] == DataManager.gameData.groupID)
                {
                    DataManager.medalHonor = true;
                    StartCoroutine(DataManager.instance.SaveRequest());
                }
                notUpdated = false;
            }
        }
    }


    public static IEnumerator GetLeaderBoard(string trackName)
    {
        //get data
        Debug.Log("Getting Data");
        string url = "https://stat2games.sites.grinnell.edu/php/getkartleaderboard.php?track=" + trackName;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        try
        {
            string data = www.downloadHandler.text;
            //parses the data into an array of strings to be made accessible
            splitData = data.Split(new char[] { ',' }, System.StringSplitOptions.None);
        }
        catch (System.Exception e)
        {
            Debug.Log("Fetching Leaderboard failed.  Error Message: " + e.Message);
        }
        isReturned = true;
    }
}
