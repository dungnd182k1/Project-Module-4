using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    [SerializeField] Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(ClickBackButton);
    }
    void ClickBackButton()
    {
        UIManager.Instance.OnEnablePanelPauseGame();
        UIManager.Instance.OnDisablePanelOptions();
    }
}
