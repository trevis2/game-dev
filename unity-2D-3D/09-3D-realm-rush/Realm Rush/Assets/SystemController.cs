using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemController : MonoBehaviour
{

    public GameObject exitPanel;
    public Button yesButton;
    public Button noButton;

    void Start()
    {
        yesButton.onClick.AddListener(QuitGame);
        noButton.onClick.AddListener(ClosePanel);
        exitPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ShowExitPanel();
        }
    }

    void ShowExitPanel()
    {
        exitPanel.SetActive(true);
    }

    void ClosePanel()
    {
        exitPanel.SetActive(false);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
