using UnityEngine;

public class WizardAttack : Enemy
{
    [SerializeField] GameObject FireBallPrefab;
    Animator animator;

    float oldSpeed;

    private void Awake()
    {
        animator = transform.GetChild(1).GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStopMovement = GetComponent<EnemyStopMovement>();
    }
    private void Start()
    {
        InvokeRepeating("Attack", 2, 4);

        oldSpeed = enemyMovement.GetSpeed();

        player = enemyMovement.GetPlayer();
    }
    private void Update()
    {

        if (enemyStopMovement.GetDistance() < 4)
        {
            enemyMovement.SetAway(true);
            enemyStopMovement.MoveAwayFromPlayer();
        }
        else if (enemyStopMovement.GetDistance() > 6)
        {
            enemyMovement.SetAway(false);
        }

        if (enemyStopMovement.GetDistance() > 3 && enemyMovement.GetAway())
        {
            if (enemyMovement.GetSpeed() > 2)
            {
                return;
            }
            enemyMovement.SetSpeed(oldSpeed + 0.5f);
        }
        else
        {
            enemyMovement.SetSpeed(oldSpeed);
        }
    }

    void Attack()
    {
        CheckPlayer();
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            CreateFireBall();
        }
    }

    void CreateFireBall()
    {
        int randomAttack = Random.Range(0,3);

        Vector2 fireBallGameObjectTransform = transform.position;

        //fireBallGameObjectTransform = new Vector2(fireBallGameObjectTransform.x, fireBallGameObjectTransform.y);

        for (int i = 0; i < 2; i++)
        {
            GameObject fireBallGameObject = Instantiate(FireBallPrefab, fireBallGameObjectTransform, Quaternion.Euler(Vector3.forward));
            fireBallGameObject.transform.localScale = new Vector3(2.5f, 2.5f, 1);

            Vector2 fireBallTransform = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

            float angle = Mathf.Atan2(fireBallTransform.y, fireBallTransform.x) * Mathf.Rad2Deg;

            Rigidbody2D fireBallRb = fireBallGameObject.AddComponent<Rigidbody2D>();
            fireBallRb.gravityScale = 0;

            fireBallGameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            fireBallGameObject.transform.SetParent(null, false);

            fireBallRb.velocity = fireBallGameObject.transform.right * 4;

            fireBallGameObjectTransform = new Vector2(fireBallGameObjectTransform.x, fireBallGameObjectTransform.y - 1);

            Destroy(fireBallGameObject, 5);
        }
    }
}
