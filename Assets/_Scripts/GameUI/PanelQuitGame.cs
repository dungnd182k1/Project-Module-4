using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelQuitGame : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    private void Awake()
    {
        yesButton.onClick.AddListener(ClickYesButton);
        noButton.onClick.AddListener(ClickNoButton);
    }
    void ClickNoButton()
    {
        UIManager.Instance.OnDisablePanelQuitGame();
    }
    void ClickYesButton()
    {

    }
}
