using UnityEngine;

public class GroundUnit : MonoBehaviour, IOnGameStates
{
    IMapGenerattable mapGenerator;

    [SerializeField]
    float offset;

    public float _offset { get => offset; }

    public void OnGameStart(params object[] parameter)
    {
        foreach (var obj in parameter)
        {
            if (obj is IMapGenerattable generator)
            {
                mapGenerator = generator;
                Debug.Log(mapGenerator);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GroundGenerator.instance.GenerateUnits(() => transform.position);
        //mapGenerator.GenerateUnits(() => transform.position);
    }
}