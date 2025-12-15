using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : MonoBehaviour
{
    public int n;

    public void StartScene(int number)
    {
        SceneManager.LoadScene(number);
    }
}
