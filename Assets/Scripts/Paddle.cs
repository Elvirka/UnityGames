using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16;
    private SpriteRenderer rend;
    private float halfWidth;
    private GameSession theGameSession;
    private Ball theBall;
    
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
        if (theGameSession.isActiveAndEnabled && theBall.IsStarted())
        {
            posInUnits = theBall.GetXPos();
        }
        else
        {
            posInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }

        return posInUnits;
    }
}
