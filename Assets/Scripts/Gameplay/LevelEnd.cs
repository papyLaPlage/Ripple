﻿using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {

    public static string levelParticle = "Level";
    [SerializeField] private int nextLevelID;
    [SerializeField] private bool isLastLevel;

    bool isActive;

    void Start()
    {
        isActive = true;
    }

	void OnTriggerEnter2D(Collider2D co)
    {
        StartCoroutine(TransitionPhase());
    }

    IEnumerator TransitionPhase()
    {
        Camera.main.GetComponent<GameCamera>().Stop();
        while (Time.timeScale > 0f)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime, 0f, 1f);
            yield return false;
        }
        Time.timeScale = 0f;
        Destroy(GameObject.Find("Level"));

        Application.LoadLevelAdditive(levelParticle+nextLevelID.ToString());
    }
}