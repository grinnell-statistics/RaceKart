using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/*
 * Class: DataManager
 *       - collects data from different parts of the game and arranges it
 */
public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public static Data.datum gameData;
    public static int x;
    public static int y;
    public static int engineChoice;

    public static int levelUnlocked;
    public static bool medalHonor;

    private void Awake()
    {
        //singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        //persistent
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        levelUnlocked = 1;
        medalHonor = false;
    }

    // When player logs out, resets these values in DataManager to avoid progress saving error
    public static void ResetData()
    {
        levelUnlocked = 1;
        medalHonor = false;
    }

    // When exiting garage scene, these values have to be resetted. 
    public static void ResetValues()
    {
        x = 0;
        y = 0;
        engineChoice = 0;
    }

    // Names of wheel, body, engine set according to chosen values. 
    public static void SetComponents()
    {

        if (string.Compare (DataManager.gameData.levelID, "Sandbox") == 0)
        {
            gameData.wheel = "Sandbox";
            gameData.body = "Sandbox";
            gameData.engine = "Sandbox";
        }
        else {
            switch (x) {
                case 0:
                    gameData.wheel = "Classic";
                    break;
                case 1:
                    gameData.wheel = "Roadster";
                    break;
                case 2:
                    gameData.wheel = "4x4";
                    break;
                case 3:
                    gameData.wheel = "Muscle";
                    break;
            }
            switch (y)
            {
                case 0:
                    gameData.body = "Classic";
                    break;
                case 1:
                    gameData.body = "Roadster";
                    break;
                case 2:
                    gameData.body = "4x4";
                    break;
                case 3:
                    gameData.body = "Muscle";
                    break;
            }
            switch (engineChoice)
            {
                case 0:
                    gameData.engine = "TheRev";
                    break;
                case 1:
                    gameData.engine = "Tube";
                    break;
                case 2:
                    gameData.engine = "Corazon";
                    break;
                case 3:
                    gameData.engine = "//";
                    break;
            }
        }
    }

    // Saves player game progress
    public IEnumerator SaveRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", gameData.playerID);
        form.AddField("groupID", gameData.groupID);
        form.AddField("levelComplete", levelUnlocked);
        form.AddField("medalHonor", medalHonor? 1 : 0);

        string url = "https://stat2games.sites.grinnell.edu/php/sendKartSaveData.php";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Player save created successfully.");
        }
    }

    // Loads player game progress
    public IEnumerator LoadRequest(string pID, string gID)
    {
        //get data
        Debug.Log("Getting Data");
        string url = "https://stat2games.sites.grinnell.edu/php/getKartSaveData.php?playerID=" + pID + "&groupID=" + gID;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string[] splitData;

        try
        {
            string data = www.downloadHandler.text;
            //csv-ify data
            splitData = data.Split(new char[] { ',' }, System.StringSplitOptions.None);
            if (splitData[0] != null)
            {
                levelUnlocked = int.Parse(splitData[0]);
            }
            if (int.Parse(splitData[1]) == 1)
            {
                medalHonor = true;
            }
            else
            {
                medalHonor = false;
            }
        }
        catch (System.Exception e)
        {
            StartCoroutine(SaveRequest());
            Debug.Log("Fetching Level failed.  Error Message: " + e.Message);
        }
        SceneManager.LoadScene("LevelScene");
    }

    // Sends player game data
    public IEnumerator SendData()
    {
        Debug.Log("Sending Data...");
        gameData.date = DateTime.Now;
        WWWForm form = new WWWForm();
        form.AddField("date", gameData.date.ToString());
        form.AddField("playerID", gameData.playerID);
        form.AddField("groupID", gameData.groupID);
        form.AddField("level", gameData.levelID);
        form.AddField("track", gameData.level);
        form.AddField("obstacles", gameData.obstacles);
        form.AddField("surface", gameData.surface);
        form.AddField("tire", gameData.wheel);
        form.AddField("engine", gameData.engine);
        form.AddField("body", gameData.body);
        form.AddField("finished", gameData.win? 1 : 0);
        form.AddField("finishedTime", gameData.time.ToString());
        form.AddField("topSpeed", gameData.topSpeed.ToString());
        form.AddField("timeToTopSpeed", gameData.timeTopSpeed.ToString());
        form.AddField("pathCurvature1", gameData.curve1);
        form.AddField("checkPoint1", gameData.c1.ToString());
        form.AddField("pathCurvature2", gameData.curve2);
        form.AddField("checkPoint2", gameData.c2.ToString());
        form.AddField("pathCurvature3", gameData.curve3);
        form.AddField("checkPoint3", gameData.c3.ToString());
        form.AddField("speed", gameData.maxVel.ToString());
        form.AddField("acceleration", gameData.acceleration.ToString());
        form.AddField("accCurve", gameData.accelCurve.ToString());
        form.AddField("suspension", gameData.suspension.ToString());
        form.AddField("drag", gameData.drag.ToString());
        form.AddField("steer", gameData.steer.ToString());
        form.AddField("grip", gameData.grip.ToString());
        form.AddField("gravity", gameData.gravity.ToString());
        form.AddField("ramps", gameData.ramps.ToString());
        form.AddField("challenge1", gameData.challenge1);
        form.AddField("challenge2", gameData.challenge2);
        form.AddField("restart", gameData.restart.ToString());
        
        string url = "https://stat2games.sites.grinnell.edu/php/sendkartinfo.php";
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Player data created successfully");
        }

        // checks if the player has completed level 1, if yes, activate level2
        if (gameData.level.Contains("B") && (levelUnlocked < 2))
        {
            levelUnlocked = 2;
        }
        // checks if the player has completed level 2, if yes, activate level3
        if (gameData.level.Contains("W") && (levelUnlocked < 3))
        {
            levelUnlocked = 3;
        }
            StartCoroutine(SaveRequest());

        yield return Leaderboard.GetLeaderBoard(gameData.level);
    }

    // Used for checking if the values were correctly inputted or not
    public static void DebugAll()
    {
        Debug.Log("playerID: " + gameData.playerID);
        Debug.Log("groupID: " + gameData.groupID);
        Debug.Log("track: " + gameData.level);
        Debug.Log("obstacles: " + gameData.obstacles);
        Debug.Log("surface: " + gameData.surface);
        Debug.Log("wheel: " + gameData.wheel);
        Debug.Log("engine: " + gameData.engine);
        Debug.Log("body: " + gameData.body);
        Debug.Log("win: " + gameData.win);
        Debug.Log("time: " + gameData.time);
        Debug.Log("topSpeed: " + gameData.topSpeed);
        Debug.Log("timeToTopSpeed: " + gameData.timeTopSpeed);
        Debug.Log("curve1: " + gameData.curve1);
        Debug.Log("checkpoint1: " + gameData.c1.ToString());
        Debug.Log("curve2: " + gameData.curve2);
        Debug.Log("checkpoint2: " + gameData.c2.ToString());
        Debug.Log("curve3: " + gameData.curve3);
        Debug.Log("checkpoint3: " + gameData.c3.ToString());
        Debug.Log("TopSpeed: " + gameData.maxVel);
        Debug.Log("Acceleration: " + gameData.acceleration);
        Debug.Log("AccelerationCurve: " + gameData.accelCurve);
        Debug.Log("Suspension: " + gameData.suspension);
        Debug.Log("Drag: " + gameData.drag);
        Debug.Log("Steer: " + gameData.steer);
        Debug.Log("Grip: " + gameData.grip);
        Debug.Log("Gravity: " + gameData.gravity);
        Debug.Log("Ramps: " + gameData.ramps);
        Debug.Log("Challenge1: " + gameData.challenge1);
        Debug.Log("Challenge2: " + gameData.challenge2);
        Debug.Log("Restart: " + gameData.restart);
        
    }

}
