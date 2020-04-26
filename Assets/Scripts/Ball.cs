using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 1f;
    [SerializeField] private float yPush = 10f;
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] public float randomFactor = 0.2f;
    
    private Vector2 paddleToBallVector;
    private bool isStarted;

    private AudioSource myAudioSource;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        isStarted = false;
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            LaunchOnMouseClick();
            LockToPaddle();
        }
    }

    private void LaunchOnMouseClick()
    {
        //Input.GetMouseButtonDown(0)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 velocityTweak = new Vector2(
            Random.Range(-randomFactor, randomFactor), 
            Random.Range(-randomFactor, randomFactor));
        
        if (isStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)]; 
            myAudioSource.PlayOneShot(clip);
            myRigidbody.velocity += velocityTweak;
        }
    }

    public bool IsStarted()
    {
        return isStarted;
    }

    public float GetXPos()
    {
        return transform.position.x;
    }

}
