using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public string sceneName;


    public void LevelCompleteFunc()
    {
            SceneManager.LoadScene(sceneName);
    }
}
