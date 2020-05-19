using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }   
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }
    
    
    public int GetScore()
    {
        return score;
    }
    
    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
