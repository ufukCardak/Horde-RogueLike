using System.Collections;
using UnityEngine;

public class Boss2Attack : Enemy
{
    [SerializeField] GameObject SpellSkillPrefab,FireBallPrefab,Enemy7Prefab, coinPrefab;
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
        else if(enemyStopMovement.GetDistance() > 6)
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
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                StartCoroutine(SpawnSpellSkill());
                break;

            case 1:
                if (!enemyStopMovement.CheckDistance(3))
                {
                    return;
                }
                StartCoroutine(AttackMelee());
                break;

            case 2:
                CreateFireBall();
                break;

            default:
                break;
        }
    }

    void CreateFireBall()
    {
        int y = 2;
        int randomAttack = Random.Range(0, 1);

        if (randomAttack == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject enemy7GameObject = Instantiate(Enemy7Prefab, new Vector2(transform.position.x, transform.position.y + y), Quaternion.identity);
                enemy7GameObject.GetComponent<EnemyMovement>().SetPlayer(player);
                enemy7GameObject.GetComponent<EnemyHealth>().SetEnemyHealthExp(1,0, 0, false, null);
                Destroy(enemy7GameObject, 5);

                y = -2;
            }
        }

        Vector2 fireBallGameObjectTransform = transform.position;

        fireBallGameObjectTransform = new Vector2(fireBallGameObjectTransform.x, fireBallGameObjectTransform.y + 1);

        for (int i = 0; i < 3; i++)
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

    IEnumerator SpawnSpellSkill()
    {
        if (player == null)
            player = enemyMovement.GetPlayer();

        animator.SetTrigger("canCast");
        enemyMovement.SetSkill(true);

        yield return new WaitForSeconds(2f);

        float random = 1;
        for (int i = 0; i < Random.Range(5, 8); i++)
        {
            Vector2 randomPlayerTransform = new Vector2(player.transform.position.x + Random.Range(-random, random + 1), player.transform.position.y + Random.Range(-random, random + 1));
            GameObject spellSkillGameObject = Instantiate(SpellSkillPrefab, randomPlayerTransform, Quaternion.identity);
            Destroy(spellSkillGameObject, 1.5f);
            if (random < 5)
            {
                random += 1f;
            }
        }

        yield return new WaitForSeconds(1f);

        enemyMovement.SetSkill(false);
    }

    IEnumerator AttackMelee()
    {
        enemyMovement.SetSkill(true);
        animator.SetTrigger("canAttack");

        yield return new WaitForSeconds(1f);

        enemyMovement.SetSkill(false);
    }

    private void OnDestroy()
    {
        GameObject coinGameObject = Instantiate(coinPrefab, transform);
        coinGameObject.GetComponent<Coin>().SetCoinMultiplier(4);
        coinGameObject.transform.SetParent(null, false);
    }
}
