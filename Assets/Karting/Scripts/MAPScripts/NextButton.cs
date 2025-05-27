using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//   Class: NextButton
// Purpose: Manages the text that appears in when the next button is clicked
public class NextButton : MonoBehaviour
{
    public GameObject[] messages;
    public Button playButton;
    public Button nextButton;
    public Button backButton;
    public GameObject controls;
    public int clickNoNext;

    public void Start()
    {
        clickNoNext = 0;
    }

    // Function: GoToNextMessage
    //  Purpose: Goes to the next message when next button is clicked
    public void GoToNextMessage()
    {
        Debug.Log("Click Numbers " + clickNoNext);  

        if (clickNoNext == (messages.Length-2))
        {
            nextButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            controls.SetActive(true);
        }
            
        messages[clickNoNext].SetActive(false);
        clickNoNext++;
        messages[clickNoNext].SetActive(true);

        if (clickNoNext == 1)
        {
            backButton.gameObject.SetActive(true);
        }
    }

    // Function: GoToPreviousMessage
    //  Purpose: Goes to the previous message when back button is clicked
    public void GoToPreviousMessage()
    {
        Debug.Log("previous message clicked");
        messages[clickNoNext].SetActive(false);
        Debug.Log("hello: message :: " + messages[clickNoNext]);
        clickNoNext--;
        messages[clickNoNext].SetActive(true);

        if (clickNoNext == 0) 
        {
            backButton.gameObject.SetActive(false);
        }
    }
}
