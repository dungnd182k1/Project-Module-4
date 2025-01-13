using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] ConfigPowerUp[] powerUp;
    [SerializeField] ConfigPowerUp buff; 
    public ConfigPowerUp buff1;
    public ConfigPowerUp buff2;
    public ConfigPowerUp buff3;

    public void ChooseBuff1()
    {
        buff = buff1;
        UIManager.Instance.AddToBuffList(buff);
    }
    public void ChooseBuff2()
    {
        buff = buff2;
        UIManager.Instance.AddToBuffList(buff);
    }
    public void ChooseBuff3()
    {
        buff = buff3;
        UIManager.Instance.AddToBuffList(buff);
    }
    public ConfigPowerUp GetConfigPowerUp()
    {
        int levelBuff;
        float percent = Random.Range(0, 100);
        if (percent < 1) levelBuff = 6;
        else if (percent < 4) levelBuff = 5;
        else if (percent < 10) levelBuff = 4;
        else if (percent < 20) levelBuff = 3;
        else if (percent < 40) levelBuff = 2;
        else if (percent < 70) levelBuff = 1;
        else levelBuff = 0;
        while (true)
        {
            int index = Random.Range(0, powerUp.Length - 1);
            if (powerUp[index].level == levelBuff)
            {
                return powerUp[index];
            }            
        }
    }
    private void OnDrawGizmosSelected()
    {
        powerUp = Resources.LoadAll<ConfigPowerUp>("ConfigBuff");
    }   

}


