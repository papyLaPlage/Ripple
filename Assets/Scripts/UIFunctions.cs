﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        MyGUIManager.Instance.GuiState = eGUIScreen.E_MainMenu;

        DontDestroyOnLoad(this);
	}

    public void StartLevel(int ID)
    {
        MyGUIManager.Instance.GuiState = eGUIScreen.E_None;
        StartCoroutine(TransitionPhase(ID));
    }

    IEnumerator TransitionPhase(int ID)
    {
        Camera.main.GetComponent<GameCamera>().Stop();
        while (Time.timeScale > 0f)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime, 0f, 1f);
            yield return false;
        }
        Destroy(GameObject.Find("Level"));

        Application.LoadLevelAdditive(LevelEnd.levelParticle+ID.ToString());
    }
}