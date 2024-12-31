using UnityEngine;

public class GroundUnit : MonoBehaviour
{
    Vector3 UnitPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        GroundGenerator.instance.GenerateUnits(() => UnitPosition());
    }
}
