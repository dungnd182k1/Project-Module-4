using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public static GroundGenerator instance;

    [SerializeField]
    GameObject groundUnit;
    GameObject poolObj;
    List<GameObject> groundUnitPool;
    int inPoolCount = 3;
    HashSet<Vector3> generateCoordinates;
    Vector3 pivotPosition = Vector3.zero;
    [SerializeField]
    float offset = 18;

    List<GameObject> outOfRegionUnits;
    private void Awake()
    {
        instance = this;

        groundUnitPool = new List<GameObject>();
        poolObj = new GameObject("GroundUnitPool");
        for (int i = 0; i < inPoolCount; i++)
        {
            GameObject obj = Instantiate(groundUnit, poolObj.transform);
            obj.SetActive(false);
            groundUnitPool.Add(obj);
        }
        generateCoordinates = new HashSet<Vector3>();
        outOfRegionUnits = new List<GameObject>();

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
        CreateNeighborCoordinates(isFirstInit);
        CreateGenerateCoordinates(isFirstInit);
        if (generateCoordinates.Count == 0)
        {
            return;
        }
        SpawnUnits(isFirstInit);
    }

    void CreateNeighborCoordinates(bool isFirstInit)
    {
        if (generateCoordinates.Count > 0)
        {
            generateCoordinates.Clear();
        }

        for (float i = pivotPosition.z - offset; i <= pivotPosition.z + offset; i += offset)
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
}