using UnityEngine;
using System.Collections;

public class LevelInitializer : MonoBehaviour {

    [SerializeField] private float maxDepthAllowed = -10f;
    [SerializeField] private bool openMenu;

	// Use this for initialization
	IEnumerator Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObject>().Reset();
        PlayerObject.maxDepthAllowed = maxDepthAllowed;
        Camera.main.GetComponent<GameCamera>().FollowPlayer();

        while (Time.timeScale < 1f)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale + Time.unscaledDeltaTime, 0f, 1f);
            yield return false;
        }

        if (openMenu)
            MyGUIManager.Instance.GuiState = eGUIScreen.E_MainMenu;
	}
	
}
