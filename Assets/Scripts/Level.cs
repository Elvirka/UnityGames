using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int blocksCount;

    private ScenesLoader scenesLoader;

    private void Start()
    {
        scenesLoader = GetComponent<ScenesLoader>();
    }

    public void CountBlocks()
    {
        blocksCount++;
    }
    
    public void CountDestroyedBlocks()
    {
        blocksCount--;
        if (blocksCount <= 0)
        {
            string qwe = scenesLoader.GetActiveScene().name;
            if (scenesLoader.GetActiveScene().name == "Level 2")
            {
                scenesLoader.LoadWinScene();
            }
            else
            {
                scenesLoader.LoadNextScene();
            }
        }
    }
}
