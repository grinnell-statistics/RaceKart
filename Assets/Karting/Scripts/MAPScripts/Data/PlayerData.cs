using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

//   Class: PlayerData
// Purpose: To input the player information (Group ID and player ID) to DataManager's player data, to filter out bad words
public class PlayerData : MonoBehaviour
{
    public GameObject badWordMenu;
    public GameObject commaMenu;
    public GameObject message;
    public Text pID;
    public Text gID;
    private bool groupSet;
    private bool playerSet;

    private string[] badWords;
    [SerializeField] public TextAsset badWordsFile;


    // initialize variables
     public void Start()
    {
         message.SetActive(false);
         groupSet = false;
         playerSet = false;
     }

    // splits the badword file into arrays for checking purposes
    public void Awake()
    {   
        badWords = badWordsFile.text.Split(',');
        for (int i = 0; i < badWords.Length; i++)
        {
            badWords[i] = badWords[i].Replace(" ", string.Empty);
            badWords[i] = badWords[i].ToLower();
        }
       
    }


    // check if the playerID and groupId aren't bad words, also checks if they are empty or not.
    public void EnterIDs()
    {

        if (IsBadWord(pID.text) || IsBadWord(gID.text))
        {
            // if bad word, display bad word message
            badWordMenu.SetActive(true);
        }
        else if ((ContainComma(pID.text) || (ContainComma(gID.text))))
        {
            commaMenu.SetActive(true);
        }
        else if (GroupSet() && PlayerSet() && (!IsBadWord(pID.text)) && (!IsBadWord(gID.text)))
        {
            badWordMenu.SetActive(false);
            DataManager.gameData.playerID = pID.text;
            DataManager.gameData.groupID = gID.text;
            StartCoroutine(DataManager.instance.LoadRequest(pID.text.ToString(), gID.text.ToString()));
        }
        else 
        {
            // if empty, display message
            message.SetActive(true);
        }
    }


     /// <summary>
    /// Checks to see if the corresponding word matches with any words in the bad word file.
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    private bool IsBadWord(string word)
    {
        word = word.ToLower();
        //Removes whitespace. Another method might be better (splitting the word and checking each)
        word = word.Replace(" ", string.Empty);
        
        int left, right;
        left = 0;
        right = badWords.Length - 1;

        while (right >= left)
        {
            if (word.Length <= 3 && word == badWords[left])
            {
                Debug.Log(badWords[left]);
                return true;
            }
            else if (word.Length > 3 && badWords[left].Length > 2 && word.Contains(badWords[left]))
            {
                return true;
            }
            else
                left++;
        }
        
        return false;
    }

    private bool ContainComma (string word)
    {
        return word.Contains(",");
    }
    // checks if groupID is filled or not
    public bool GroupSet()
    {
        groupSet = (gID.text.Length != 0);
        return groupSet;
    }


    // checks if playerID is filled or not
    public bool PlayerSet()
    {
        playerSet = (pID.text.Length!= 0);
        return playerSet;
    }

}
