using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameGate : MonoBehaviour
{
    [SerializeField]
    int targetSceneIndex;

    private void OnTriggerExit(Collider other)
    {
        SceneManager.LoadScene(targetSceneIndex);
    }
}
