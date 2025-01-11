using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPowerUp : MonoBehaviour
{
    [SerializeField] Button powerUp1;
    [SerializeField] Button powerUp2;
    [SerializeField] Button powerUp3;
    [SerializeField] Button roll;

    private void Awake()
    {
        powerUp1.onClick.AddListener(ClickPowerUp1);
        powerUp2.onClick.AddListener(ClickPowerUp2);
        powerUp3.onClick.AddListener(ClickPowerUp3);
        roll.onClick.AddListener(ClickRoll);
    }
    void ClickPowerUp1()
    {
        UIManager.Instance.OnDisablePanelPowerUp();
    }
    void ClickPowerUp2()
    {
        UIManager.Instance.OnDisablePanelPowerUp();
    }
    void ClickPowerUp3()
    {
        UIManager.Instance.OnDisablePanelPowerUp();
    }
    void ClickRoll()
    {

    }
}
