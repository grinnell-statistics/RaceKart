using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 *   Class: InGameMenuManager
 * Purpose: Handles the different options in the In Game Menu
 * Modified by: Reina 
 *              - to add the mute button for music / engine 
*/

public class InGameMenuManager : MonoBehaviour
{
    [Tooltip("Root GameObject of the menu used to toggle its activation")]
    public GameObject menuRoot;
    [Tooltip("Master volume when menu is open")]
    [Range(0.001f, 1f)]
    public float volumeWhenMenuOpen = 0.5f;
    [Tooltip("Toggle component for shadows")]
    public Toggle shadowsToggle;
    [Tooltip("Toggle component for framerate display")]
    public Toggle framerateToggle;
    [Tooltip("GameObject for the controls")]

    /* New Toggles added for the Music + Engine Sound*/
    public Toggle soundToggle;
    [Tooltip("GameObject for the music")]
    public Toggle engineSoundToggle;
    [Tooltip("GameObject for the engine sound")]

    public GameObject controlImage;

    public static bool engineIsOn;

    //PlayerInputHandler m_PlayerInputsHandler;
    FramerateCounter m_FramerateCounter;

    void Start()
    {
        //m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();

        m_FramerateCounter = FindObjectOfType<FramerateCounter>();

        menuRoot.SetActive(false);

        shadowsToggle.isOn = QualitySettings.shadows != ShadowQuality.Disable;
        shadowsToggle.onValueChanged.AddListener(OnShadowsChanged);

        framerateToggle.isOn = m_FramerateCounter.uiText.gameObject.activeSelf;
        framerateToggle.onValueChanged.AddListener(OnFramerateCounterChanged);
    }

    private void Update()
    {
        //passes info whether engine is on or not
        engineIsOn = engineSoundToggle.isOn;

        // if the sound toggle is unchecked, set volume to 0
        // else activate the sound
        if (!soundToggle.isOn)
        {
            AudioUtility.SetMasterVolume(0);
        }
        else
        {
            AudioUtility.SetMasterVolume(volumeWhenMenuOpen);
        }

        if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu)
            || (menuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
        {
            if (controlImage.activeSelf)
            {
                controlImage.SetActive(false);
                return;
            }

            SetPauseMenuActivation(!menuRoot.activeSelf);

        }

        if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                shadowsToggle.Select();
            }
        }
    }

    /* closes the pause menu */
    public void ClosePauseMenu()
    {
        SetPauseMenuActivation(false);
    }


    public void TogglePauseMenu()
    {
        SetPauseMenuActivation(!menuRoot.activeSelf);
    }

    void SetPauseMenuActivation(bool active)
    {
        menuRoot.SetActive(active);

        if (menuRoot.activeSelf)
        {
            Time.timeScale = 0f;

            if (soundToggle.isOn)
            {
                AudioUtility.SetMasterVolume(volumeWhenMenuOpen);
            }
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            Time.timeScale = 1f;

            if (soundToggle.isOn)
            {
                AudioUtility.SetMasterVolume(volumeWhenMenuOpen);
            }
        }
    }

    void OnShadowsChanged(bool newValue)
    {
        QualitySettings.shadows = newValue ? ShadowQuality.All : ShadowQuality.Disable;
    }

    void OnFramerateCounterChanged(bool newValue)
    {
        m_FramerateCounter.uiText.gameObject.SetActive(newValue);
    }

    public void OnShowControlButtonClicked(bool show)
    {
        controlImage.SetActive(show);
    }
}