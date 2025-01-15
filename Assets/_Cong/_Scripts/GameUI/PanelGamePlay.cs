using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelGamePlay : MonoBehaviour
{
    [SerializeField] Button pauseButton;

    private void Awake()
    {
        pauseButton.onClick.AddListener(ClickPauseButton);
    }
    void ClickPauseButton()
    {
        AudioManager.Instance.ClickButton();
        UIManager.Instance.OnEnablePanelPauseGame();
    }
}
