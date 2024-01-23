using System.Collections;
using UnityEngine;

public class EnemyAttackRange : Enemy
{
    bool enemyAttackDelay = true;

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] int attackSpeed;
    EnemySpawn enemySpawn;
    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStopMovement = GetComponent<EnemyStopMovement>();
    }

    private void Start()
    {
        player = enemyMovement.GetPlayer();

        enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.SetEnemyHealth(enemyHealth.GetEnemyHealth() / 10);
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

        if (enemyStopMovement.GetDistance() <= 6 && enemyAttackDelay)
        {
            StartCoroutine(AttackDelay());
            Attack();
        }
    }

    void Attack()
    {
        CheckPlayer();

        int rangeSpeed = 2;

        GameObject rangeAttack = Instantiate(arrowPrefab,transform.position,Quaternion.Euler(Vector3.forward));

        Vector2 rangeAttackTransform = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

        float angle = Mathf.Atan2(rangeAttackTransform.y, rangeAttackTransform.x) * Mathf.Rad2Deg;

        Rigidbody2D arrowRb = rangeAttack.AddComponent<Rigidbody2D>();
        arrowRb.gravityScale = 0;

        rangeAttack.transform.rotation = Quaternion.Euler(0, 0, angle);
        rangeAttack.transform.SetParent(null,false);

        if (rangeAttack.tag == "Arrow")
        {
            rangeSpeed = 4;
        }

        arrowRb.velocity = rangeAttack.transform.right * rangeSpeed;

        Destroy(rangeAttack, 5);
    }
    IEnumerator AttackDelay()
    {
        enemyAttackDelay = false;

        yield return new WaitForSeconds(attackSpeed);

        enemyAttackDelay = true;

    }


    public void SetEnemySpawn(EnemySpawn enemySpawn)
    {
        this.enemySpawn = enemySpawn;
    }

    private void OnDestroy()
    {
        if (enemySpawn == null)
        {
            return;
        }
        enemySpawn.SetEnemyRangeCount();
    }
}
