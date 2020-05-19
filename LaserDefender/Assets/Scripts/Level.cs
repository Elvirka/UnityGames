using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float delayInSeconds = 1f;
    [SerializeField] private GameSession gameSession;
    
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }
    
    
    public void LoadGameOver()
    {
        StartCoroutine(DelayLoadGameOver());
    }

    private IEnumerator DelayLoadGameOver()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
        
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
