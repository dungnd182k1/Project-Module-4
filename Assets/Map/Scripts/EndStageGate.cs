using UnityEngine;

public class EndStageGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GroundGenerator.instance.ChangeMap();
        //GameManager.instance.SetGameState(GameState.StageStart);
    }
}
