using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMapCreator : MonoBehaviour
{
    GameObject map;
    void Awake()
    {
        GameObject mapPrefab = Resources.Load<GameObject>("Town");
        map = Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);
    }

    private void OnDisable()
    {
        Destroy(map);
        Resources.UnloadUnusedAssets();
    }
}
