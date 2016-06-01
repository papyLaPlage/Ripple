using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MyGUIManager.Instance.GuiState = eGUIScreen.E_MainTitle;
	}
}
