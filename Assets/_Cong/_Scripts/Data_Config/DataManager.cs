using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] ConfigPowerUp[] powerUp;

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


