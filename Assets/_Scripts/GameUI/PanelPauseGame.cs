using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPauseGame : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(ClickResumeButton);
        optionsButton.onClick.AddListener(ClickOptionsButton);
        restartButton.onClick.AddListener(ClickRestartButton);
        quitButton.onClick.AddListener(ClickQuitButton);
    }
    void ClickResumeButton()
    {
        UIManager.Instance.OnDisablePanelPauseGame();
    }
    void ClickQuitButton()
    {
        UIManager.Instance.OnEnablePanelQuitGame();
    }
    void ClickOptionsButton()
    {
        UIManager.Instance.OnEnablePanelOptions();
        UIManager.Instance.OnDisablePanelPauseGame();
    }
    void ClickRestartButton()
    {
        
    }
}
