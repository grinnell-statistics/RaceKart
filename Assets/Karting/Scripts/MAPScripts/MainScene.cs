using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;


/*
 *   Class: MainScene
 * Purpose: Allows user to press "tab" to switch from Player ID
 *          to Group ID to the "Play" button
 * Modified by: Priyanka, Ryuta, and Reina
*/
public class MainScene : MonoBehaviour
{
    // play button gameobject
    public GameObject playButton;

    // player ID input field
    public InputField playerAlias;

    // group ID input field
    public InputField groupID;

    
    [DllImport("__Internal")]
    private static extern void OpenWindow(string url);

    public void Start()
    {
        DataManager.ResetData();
        playerAlias.contentType = InputField.ContentType.Alphanumeric;
        groupID.contentType = InputField.ContentType.Alphanumeric;
    }


    public void Update()
    {

        //allows users to use 'tab' to return to the next input field
        if(Input.GetKeyDown(KeyCode.Tab)&&playerAlias.isFocused)
        {
            groupID.ActivateInputField();
        }
        if(Input.GetKeyDown(KeyCode.Tab)&&groupID.isFocused)
        {
            playButton.GetComponent<Button>().Select();
        }

        
    }

    /* All of the methods below that open an external link
        will not work in the Unity Editor as it is made for 
        WebGL.
        
        See Link for more info :https://va.lent.in/opening-links-in-a-unity-webgl-project/ */
    // Plays video tutorial
    public void InstructorNotes()
    {

        string Url = "https://stat2labs.sites.grinnell.edu/racer.html";
        #if !UNITY_EDITOR
            OpenWindow(Url);
        #endif
        #if UNITY_EDITOR
            Application.OpenURL(Url);
        #endif
        EventSystem.current.SetSelectedGameObject(null);
    }

    //takes user to the data website
    public void GetGameData()
    {

        string Url = "https://stat2games.sites.grinnell.edu/data/racekart/racekart.php?fbclid=IwAR3pVtRUefu3GT6UMo4XJ41krJejA_hbUGiVh04op4ZzqkvxxyfcfbfUHHw";
        #if !UNITY_EDITOR
            OpenWindow(Url);
        #endif
        #if UNITY_EDITOR
            Application.OpenURL(Url);
        #endif
        EventSystem.current.SetSelectedGameObject(null);
    }

    //takes user to the data visualization website
    public void GetDataVisualization()
    {

        string Url = "http://shiny.grinnell.edu/RaceKart/";
        #if !UNITY_EDITOR
            OpenWindow(Url);
        #endif
        #if UNITY_EDITOR
            Application.OpenURL(Url);
        #endif
        EventSystem.current.SetSelectedGameObject(null);
    }

    //takes user to the tutorial website
    public void VideoTutorial()
    {

        string Url = "https://www.youtube.com/watch?v=0yRM7laTS9s";
        #if !UNITY_EDITOR
            OpenWindow(Url);
        #endif
        #if UNITY_EDITOR
            Application.OpenURL(Url);
        #endif
        EventSystem.current.SetSelectedGameObject(null);
    }



}
