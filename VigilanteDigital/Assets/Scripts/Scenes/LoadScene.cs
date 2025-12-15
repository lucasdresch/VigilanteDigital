using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int n;
    public GameObject BordPhase;
    public GameObject title;

    public void StartScene(int number)
    {
        SceneManager.LoadScene(number);
    }
}
