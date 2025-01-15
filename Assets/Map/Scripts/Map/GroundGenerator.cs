using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour, IMapGenerattable, IOnGameStates
{
    public static GroundGenerator instance;

    GameObject[] groundUnits;
    const string UNITS_PATH_IN_RESOURCES = "MapUnits";
    GameObject groundUnit;
    GameObject poolObj;
    List<GameObject> groundUnitPool;
    int inPoolCount;
    HashSet<Vector3> generateCoordinates;
    Vector3 pivotPosition = Vector3.zero;
    int mapIndex/* = -1*/;
    float offset;
    [SerializeField]
    int shell = 1;

    List<GameObject> outOfRegionUnits;
    private void Awake()
    {
        instance = this;

        groundUnits = Resources.LoadAll<GameObject>(UNITS_PATH_IN_RESOURCES);
        groundUnitPool = new List<GameObject>();
        poolObj = new GameObject("GroundUnitPool");
        generateCoordinates = new HashSet<Vector3>();
        outOfRegionUnits = new List<GameObject>();
        inPoolCount = shell * 2 + 1;
        InitMap(groundUnits[mapIndex]);
    }

    public void OnGameStart(params object[] parameter)
    {
        //groundUnits = Resources.LoadAll<GameObject>(UNITS_PATH_IN_RESOURCES);
        //groundUnitPool = new List<GameObject>();
        //poolObj = new GameObject("GroundUnitPool");
        //generateCoordinates = new HashSet<Vector3>();
        //outOfRegionUnits = new List<GameObject>();
        //inPoolCount = shell * 2 + 1;
    }

    //public void OnStageStart()
    //{
    //    ChangeMap();
    //}

    void InitMap(GameObject currentGroundUnit)
    {
        groundUnit = currentGroundUnit;
        offset = groundUnit.GetComponentInChildren<GroundUnit>()._offset;

        if (groundUnitPool.Count > 0)
        {
            foreach (GameObject unit in groundUnitPool)
            {
                Destroy(unit);
            }
            groundUnitPool.Clear();
        }
        for (int i = 0; i < inPoolCount; i++)
        {
            GameObject obj = Instantiate(groundUnit, poolObj.transform);
            //obj.GetComponentInChildren<IOnGameStates>().OnGameStart(this);
            obj.SetActive(false);
            groundUnitPool.Add(obj);
        }

        GenerateUnits();
    }

    public void GenerateUnits(Func<Vector3> PivotSetter = null)
    {
        bool isFirstInit = true;
        if (PivotSetter != null)
        {
            isFirstInit = false;
            pivotPosition = PivotSetter();
        }
        CreateNeighborCoordinates();
        CreateGenerateCoordinates(isFirstInit);
        SpawnUnits(isFirstInit);
    }

    void CreateNeighborCoordinates()
    {
        if (generateCoordinates.Count > 0)
        {
            generateCoordinates.Clear();
        }

        if (offset <= 0) offset = 20;

        for (float i = pivotPosition.z - (offset * shell); i <= pivotPosition.z + (offset * shell); i += offset)
        {
            generateCoordinates.Add(new Vector3(0, 0, i));
        }
    }

    void CreateGenerateCoordinates(bool isFirstInit)
    {
        if (isFirstInit)
        {
            return;
        }

        if (outOfRegionUnits.Count > 0)
        {
            outOfRegionUnits.Clear();
        }

        foreach (GameObject unit in groundUnitPool)
        {
            if (unit.activeSelf)
            {
                if (generateCoordinates.Contains(unit.transform.position))
                {
                    generateCoordinates.Remove(unit.transform.position);
                }
                else
                {
                    outOfRegionUnits.Add(unit);
                }
            }
        }
    }

    void SpawnUnits(bool isFirstInit)
    {
        if (generateCoordinates.Count == 0)
        {
            return;
        }

        foreach (Vector3 position in generateCoordinates)
        {
            if (isFirstInit)
            {
                if (HasValidObjectInPool(out GameObject obj))
                {
                    obj.transform.position = position;
                    obj.SetActive(true);
                }
            }
            else
            {
                outOfRegionUnits[outOfRegionUnits.Count - 1].transform.position = position;
                outOfRegionUnits.RemoveAt(outOfRegionUnits.Count - 1);
            }
        }
    }

    bool HasValidObjectInPool(out GameObject objectTakenFromPool)
    {
        objectTakenFromPool = null;
        foreach (GameObject obj in groundUnitPool)
        {
            if (!obj.activeSelf)
            {
                objectTakenFromPool = obj;
                return true;
            }
        }
        return false;
    }

    public void ChangeMap()
    {
        mapIndex++;
        mapIndex %= groundUnits.Length;
        InitMap(groundUnits[mapIndex]);
    }

    private void OnDisable()
    {
        Destroy(poolObj);
        groundUnitPool = null;
        outOfRegionUnits = null;
        groundUnit = null;
        Resources.UnloadUnusedAssets();
    }
}