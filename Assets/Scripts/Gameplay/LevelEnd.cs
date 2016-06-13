using UnityEngine;
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
        if(PlayerPrefs.GetInt("clearedLevels") < nextLevelID - 1)
            PlayerPrefs.SetInt("clearedLevels", nextLevelID-1);
        GameCamera gameCamera = Camera.main.GetComponent<GameCamera>();
        gameCamera.Stop();
        gameCamera.StartTransition();
        while (Time.timeScale > 0f)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - Time.unscaledDeltaTime, 0f, 1f);
            yield return false;
        }
        Time.timeScale = 0f;
        Destroy(GameObject.Find("Level"));

        if(isLastLevel)
            Application.LoadLevelAdditive("empty");
        else
            Application.LoadLevelAdditive(levelParticle + nextLevelID.ToString());
    }
}
