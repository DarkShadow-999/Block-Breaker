using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration Parameters

    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f, yPush = 15f;
    [SerializeField] float constantSpeed = 12f;
    [SerializeField] GameObject ballVFX;
    [SerializeField] float randomFactor = 0.2f;

    //cached reference
    

    //state

    Vector2 paddleToBallVector;
    bool hasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted == false)
        {
            LockBallToPaddle();
            LaunchBallOnClick();
        }
        GetComponent<Rigidbody2D>().velocity = constantSpeed * (GetComponent<Rigidbody2D>().velocity.normalized);
        TriggerBallVFX();
    }

    private void LaunchBallOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f,randomFactor), Random.Range(0f,randomFactor));

        if (hasStarted)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Rigidbody2D>().velocity += velocityTweak;
        }
    }

    private void TriggerBallVFX()
    {
        GameObject sparkles = Instantiate(ballVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
 }