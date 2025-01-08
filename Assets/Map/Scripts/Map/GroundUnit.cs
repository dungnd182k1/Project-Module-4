using UnityEngine;

public class GroundUnit : MonoBehaviour
{
    [SerializeField]
    float offset;

    public float _offset { get => offset; }

    private void OnTriggerEnter(Collider other)
    {
        GroundGenerator.instance.GenerateUnits(() => transform.position);
    }
}