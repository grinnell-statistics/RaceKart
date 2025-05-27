using UnityEngine;
using UnityEngine.EventSystems;

//   Class: ToggleGameObjectButton
// Purpose: To set game objects in scene active/inactive when a toggle button is clicked
// Modified by: Priyanka, Ryuta, and Reina
public class ToggleGameObjectButton : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject mainScenekart;
    public GameObject[] otherMainSceneObjects;
    public bool resetSelectionAfterClick;

    void Update()
    {
        if (objectToToggle.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel))
        {
            SetGameObjectActive(false);
            
        }
    }

    // Function: SetGameObjectActive
    //  Purpose: Set a specific game object actice when a button is clicked
    public void SetGameObjectActive(bool active)
    {
        objectToToggle.SetActive(active);

        if (mainScenekart != null)
            mainScenekart.SetActive(!active);

        if (resetSelectionAfterClick)
            EventSystem.current.SetSelectedGameObject(null);
    }


    // Function: SetOtherGameObjectsInactive
    //  Purpose: Setting game objects in otherMainSceneObjects array inactive
    public void SetOtherGameObjectsInactive()
    {
        for (int i = 0; i < otherMainSceneObjects.Length; i++)
        {
            otherMainSceneObjects[i].SetActive(false);
        }
    }

    // Function: SetOtherGameObjectsActive
    //  Purpose: Setting game objects in otherMainSceneObjects array active
    public void SetOtherGameObjectsActive()
    {
        for (int i = 0; i < otherMainSceneObjects.Length; i++)
        {
            otherMainSceneObjects[i].SetActive(true);
        }
    }
}
