using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMapCreator : MonoBehaviour
{
    GameObject map;
    const string MENU_MAP_PATH = "Town";
    void Awake()
    {
        GameObject mapPrefab = Resources.Load<GameObject>(MENU_MAP_PATH);
        map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
    }

    private void OnDisable()
    {
        Destroy(map);
        Resources.UnloadUnusedAssets();
    }
}
