using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Enemy
{
    Rigidbody2D rb;
    [SerializeField] Transform enemySprite;
    [SerializeField] Transform playerTransform;

    [SerializeField] float speed;

    [SerializeField] bool canMove = true;
    [SerializeField] bool skill,away,stun;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }

    public void SetStun(bool stun)
    {
        if (stun)
            SetRbVelocityToZero();
        
        this.stun = stun;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (speed == 0)
            speed = Random.Range(0.5f, 0.7f);

        if (player == null)
        {
            player = playerTransform;
        }
    }

    public Rigidbody2D GetRb()
    {
        return rb;
    }

    public void SetSkill(bool skill)
    {
        if (skill)
            SetRbVelocityToZero();

        this.skill = skill;
    }

    public bool GetSkill() 
    {
        return skill;
    }

    public void SetAway(bool away)
    {
        if (away)
        {
            SetRbVelocityToZero();
        }

        this.away = away;
    }

    public bool GetAway()
    {
        return away;
    }
    private void Update()
    {
        if (skill || stun)
        {
            return;
        }

        Flip();

        

        if (away)
        {
            return;
        }


        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }


        rb.velocity = (player.position - transform.position).normalized * speed;
    }

    void Flip()
    {

        Vector2 direction = (player.position - transform.position).normalized;
        if (direction.x < 0)
        {
            enemySprite.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            enemySprite.eulerAngles = new Vector3(0,0,0);
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
    public Transform GetPlayer()
    {
        return player;
    }
    public bool GetCanMove()
    {
        return canMove;
    }
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }


    protected void SetRbVelocityToZero()
    {
        rb.velocity = Vector2.zero;
    }
}
