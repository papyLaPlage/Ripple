using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour {

    //public static SaveManager Instance { get; private set; }

    int clearedLevels;

    [SerializeField] private Button selectButton;
    [SerializeField] private Button[] levelButtons;

    // Use this for initialization
    /*void Awake () {
        Instance = this;
	}*/

    public void ShowClearedLevels()
    {
        clearedLevels = PlayerPrefs.GetInt("clearedLevels");
        if (clearedLevels >= 0)
        {
            if (clearedLevels > 0)
            {
                selectButton.interactable = true;
                for (int i = 0; i <= clearedLevels; i++)
                {
                    levelButtons[i].interactable = true;
                }
            }
        }
        else
            PlayerPrefs.SetInt("clearedLevels", 0);
    }
}
