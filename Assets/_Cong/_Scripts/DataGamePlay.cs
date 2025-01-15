using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
using System.ComponentModel;

[Serializable]
public class PlayerData
{
    public double highestWave=0;
    public float volumeMusic=1;
    public float volumeVFX=1;
}

public class DataGamePlay:MonoBehaviour
{
    private string dataPath;

    public void StartDataGamePlay()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
        LoadData(); //load khi khởi động
    }

    public void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data); //chuyển data thành chuỗi json
            File.WriteAllText(dataPath, json); //tạo file nếu chưa tồn tại
    }

    public PlayerData LoadData()
    {
        if (File.Exists(dataPath))
        {
                string json = File.ReadAllText(dataPath);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                return data;
        }
        return new PlayerData(); // Trả về dữ liệu mới nếu không tìm thấy file
    }    
}
