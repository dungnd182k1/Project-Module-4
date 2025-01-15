using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject panelGamePlay;
    [SerializeField] GameObject panelPauseGame;
    [SerializeField] GameObject panelQuitGame;
    [SerializeField] GameObject panelPowerUp;
    [SerializeField] GameObject panelGameOver;
    [SerializeField] GameObject panelOptions;
    [SerializeField] CanvasGroup canvasGroupGamePlay;
    [SerializeField] CanvasGroup canvasGroupPauseGame;
    [SerializeField] CanvasGroup canvasGroupQuitGame;
    [SerializeField] CanvasGroup canvasGroupPowerUp;
    [SerializeField] CanvasGroup canvasGroupGameOver;
    [SerializeField] CanvasGroup canvasGroupOptions;
    [SerializeField] Transform selectedBuff;
    [SerializeField] List<Sprite> buffList = new List<Sprite>();
    [SerializeField] GameObject imageBuff;
    public bool hasPanelBuff;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnEnablePanelPowerUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnEnablePanelGameOver();
        }
    }
    public void OnEnablePanelGamePlay()
    {
        Show(panelGamePlay, canvasGroupGamePlay);
    }
    public void OnDisablePanelGamePlay()
    {
        Hide(panelGamePlay, canvasGroupGamePlay);
    }
    public void OnEnablePanelPauseGame()
    {
        Show(panelPauseGame, canvasGroupPauseGame);
    }
    public void OnDisablePanelPauseGame()
    {
        Hide(panelPauseGame, canvasGroupPauseGame);
    }
    public void OnEnablePanelQuitGame()
    {
        Show(panelQuitGame, canvasGroupQuitGame);
    }
    public void OnDisablePanelQuitGame()
    {
        Hide(panelQuitGame, canvasGroupQuitGame);
    }
    public void OnEnablePanelPowerUp()
    {
        Show(panelPowerUp, canvasGroupPowerUp);
        hasPanelBuff =true;
    }
    public void OnDisablePanelPowerUp()
    {
        Hide(panelPowerUp, canvasGroupPowerUp);
        hasPanelBuff = false;
    }
    public void OnEnablePanelGameOver()
    {
        Show(panelGameOver, canvasGroupGameOver);
    }
    public void OnDisablePanelGameOver()
    {
        Hide(panelGameOver, canvasGroupGameOver);
    }
    public void OnEnablePanelOptions()
    {
        Show(panelOptions, canvasGroupOptions);
    }
    public void OnDisablePanelOptions()
    {
        Hide(panelOptions, canvasGroupOptions);
    }
    public void AddToBuffList(ConfigPowerUp buff)
    {      
        buffList.Add(buff.iconBuff);
        AddBuffToPausePanel();       
    }
    public void AddBuffToPausePanel()
    {
        foreach (Transform buff in selectedBuff)
        {
            Destroy(buff.gameObject);
        }
        foreach (Sprite buff in buffList)
        {
            GameObject obj = Instantiate(imageBuff, selectedBuff);
            var iconBuff = obj.transform.GetChild(0).GetComponent<Image>(); 
            iconBuff.sprite = buff;
        }
    }
    public void ClearBuffList()
    {
        buffList.Clear();
    }
    public void Show(GameObject panel, CanvasGroup canvasGroup)
    {
        canvasGroup.alpha= 0;
        panel.SetActive(true);
        canvasGroup.DOFade(1, 0.5f);
        
    }
    public void Hide(GameObject panel, CanvasGroup canvasGroup)
    {
        canvasGroup.DOFade(0, 0.3f)
            .OnComplete(()=>DisablePanel(panel));        
    }
    void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
