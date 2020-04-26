using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)][SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int pointsPerHint = 10;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int currentScore = 0;
    [SerializeField] private bool isAutoPlayEnabled;

    private void Awake()
    {
        int gameStateObjectsCount = FindObjectsOfType<GameSession>().Length;
        if (gameStateObjectsCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore(int hintsCount)
    {
        currentScore += pointsPerHint * hintsCount;
        scoreText.text = currentScore.ToString();
    }
    
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
