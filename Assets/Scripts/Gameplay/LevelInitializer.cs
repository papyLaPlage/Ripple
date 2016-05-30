using UnityEngine;
using System.Collections;

public class LevelInitializer : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObject>().Reset();
        Camera.main.GetComponent<GameCamera>().FollowPlayer();

        while (Time.timeScale < 1f)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + Time.unscaledDeltaTime, 0f, 1f);
            yield return false;
        }
	}
	
}
