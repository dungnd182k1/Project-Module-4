using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button musicButton;
    [SerializeField] Button vfxButton;
    [SerializeField] GameObject black1;
    [SerializeField] GameObject black2;

    private void Awake()
    {
        backButton.onClick.AddListener(ClickBackButton);
        musicButton.onClick.AddListener(ClickMusicButton);
        vfxButton.onClick.AddListener(ClickVfxButton);
    }
    void ClickBackButton()
    {
        AudioManager.Instance.ClickButton();
        UIManager.Instance.OnEnablePanelPauseGame();
        UIManager.Instance.OnDisablePanelOptions();
    }
    void ClickMusicButton()
    {
        AudioManager.Instance.ClickButton();
        if (black1.activeSelf)
        {
            black1.SetActive(false);
        }
        else
        {
            black1.SetActive(true);
        }
    }
    void ClickVfxButton()
    {
        AudioManager.Instance.ClickButton();
        if (black2.activeSelf)
        {
            black2.SetActive(false);
        }
        else
        {
            black2.SetActive(true);
        }
    }
}
