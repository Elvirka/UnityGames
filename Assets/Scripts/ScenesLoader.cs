using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }
    
    public void LoadWinScene()
    {
        //int winSceneIndex = SceneManager.GetSceneByName("Win").buildIndex;
        SceneManager.LoadScene(5);
    }
    
    
    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void QuitGame()
    {    
        Application.Quit();
    }
}
