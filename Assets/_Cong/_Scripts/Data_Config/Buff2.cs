using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff2 : PowerUp
{
    protected override void ChangeInfoBuff()
    {
        if (buff != null) return;
        buff = DataManager.Instance.GetConfigPowerUp();
        iconBuff.sprite = buff.iconBuff;
        txtNameBuff.text = buff.namePowerUp;
        txtDescriptionBuff.text = buff.descriptionBuff;
        DataManager.Instance.buff2 = buff;
    }
}
