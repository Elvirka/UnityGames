using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnits = 16;
    
    private SpriteRenderer rend;
    private GameSession theGameSession;
    private Ball theBall;
    private float halfWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        halfWidth = rend.bounds.size.x/2;
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), halfWidth, screenWidthInUnits - halfWidth);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        float posInUnits;
        if (theGameSession.IsAutoPlayEnabled() && theBall.IsStarted())
        {
            posInUnits = theBall.transform.position.x;
        }
        else
        {
            posInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }

        return posInUnits;
    }
}
