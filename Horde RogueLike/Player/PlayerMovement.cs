using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : Player
{
    Rigidbody2D rb;
    Vector2 movementVector;
    [SerializeField] Transform playerSprite;
    [SerializeField] float speed;

    [SerializeField] FixedJoystick fixedJoystick;

    bool speedMutation;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        playerMovement = this;
    }
    public void SetSpeed(float newSpeed)
    {
        speed += speed * newSpeed / 100;
    }
    public Vector2 GetMovementVector()
    {
        return movementVector;
    }

    public void SetJoystick(FixedJoystick fixedJoystick)
    {
        this.fixedJoystick = fixedJoystick;
    }

    public void Slow(float slowValue,int slowTime)
    {
        if (speedMutation)
        {
            return;
        }
        StartCoroutine(SlowWait(slowValue, slowTime));
    }
    private void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //movementVector.x = Input.GetAxisRaw("Horizontal");
        //movementVector.y = Input.GetAxisRaw("Vertical");

        movementVector.x = fixedJoystick.Horizontal;
        movementVector.y = fixedJoystick.Vertical;

        rb.velocity = movementVector * speed;

        if (rb.velocity.x < 0)
        {
            playerSprite.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            playerSprite.transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
        }
    }

    IEnumerator SlowWait(float slowValue,int slowTime)
    {
        speed -= slowValue;
        yield return new WaitForSeconds(slowTime);
        speed += slowValue;
    }
}
