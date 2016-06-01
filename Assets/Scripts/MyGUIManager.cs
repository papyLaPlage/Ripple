using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public enum eGUIScreen
{
    E_None,
    E_Greyscale,
    E_MainTitle,
    E_MainMenu,
    E_LevelSelect,
    E_Credits,
    E_InGame,
    E_Pause
}

public class MyGUIManager : MonoBehaviour
{

    public static MyGUIManager Instance { get; private set; }

    public eGUIScreen GuiState { get { return guiState; } set { ShowGUI(false); guiState = value; ShowGUI(true); } }
    private eGUIScreen guiState;

    public GameObject GUIGreyscale;
    public GameObject GUIMainTitle;
    public GameObject GUIMainMenu;
    public GameObject GUILevelSelect;
    public GameObject GUICredits;
    public GameObject GUIInGame;
    public GameObject GUIPause;

    void Awake()
    {
        Instance = this;
    }

    private void ShowGUI(bool show)
    {
        //Debug.Log("Show GUI : " + guiState);
        switch (guiState)
        {
            case eGUIScreen.E_None:
                break;
            case eGUIScreen.E_MainTitle:
                GUIMainTitle.SetActive(show);
                break;
            case eGUIScreen.E_MainMenu:
                GUIMainMenu.SetActive(show);
                break;
            case eGUIScreen.E_LevelSelect:
                GUILevelSelect.SetActive(show);
                break;
            case eGUIScreen.E_Credits:
                GUICredits.SetActive(show);
                break;
            case eGUIScreen.E_InGame:
                GUIMainMenu.SetActive(show);
                break;
            case eGUIScreen.E_Pause:
                GUIMainMenu.SetActive(show);
                break;
            default:
                break;
        }
    }
}
