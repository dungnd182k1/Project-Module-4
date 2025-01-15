using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public ConfigPowerUp buff;
    [SerializeField] protected Image iconBuff;
    [SerializeField] protected Text txtNameBuff;
    [SerializeField] protected Text txtDescriptionBuff;
    public Button button;
    public void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ClickButton());
    }
    private void Update()
    {        
        if (!UIManager.Instance.hasPanelBuff)
        {
            buff = null;
            return;
        }
        ChangeInfoBuff();        
    }
    public virtual void ClickButton()
    {
        AudioManager.Instance.ClickButton();
        //Cộng chỉ số
    }
    protected virtual void ChangeInfoBuff()
    {
        if (buff != null) return;
        buff = DataManager.Instance.GetConfigPowerUp();
        iconBuff.sprite = buff.iconBuff;
        txtNameBuff.text = buff.namePowerUp;
        txtDescriptionBuff.text = buff.descriptionBuff;
    }    
}
