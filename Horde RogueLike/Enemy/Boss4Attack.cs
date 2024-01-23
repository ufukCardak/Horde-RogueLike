using System.Collections;
using UnityEngine;

public class Boss4Attack : Enemy
{
    [SerializeField] GameObject coinPrefab;

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
    }
    private void Update()
    {
        animator.SetBool("canRun", enemyMovement.GetCanMove());

        if (enemyStopMovement.GetDistance() > 3)
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
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                if (!enemyStopMovement.CheckDistance(3))
                {
                    return;
                }
                StartCoroutine(AttackMelee1());
                break;

            case 1:
                if (!enemyStopMovement.CheckDistance(3))
                {
                    return;
                }
                StartCoroutine(AttackMelee2());
                break;

            case 2:
                if (!enemyStopMovement.CheckDistance(3))
                {
                    return;
                }
                StartCoroutine(AttackMelee3());
                break;

            case 3:
                break;

            default:
                break;
        }
    }
    IEnumerator AttackMelee1()
    {
        enemyMovement.SetSkill(true);
        animator.SetTrigger("canAttack1");

        yield return new WaitForSeconds(0.4f);

        enemyMovement.SetSkill(false);
    }
    IEnumerator AttackMelee2()
    {
        enemyMovement.SetSkill(true);
        animator.SetTrigger("canAttack2");

        yield return new WaitForSeconds(0.4f);

        enemyMovement.SetSkill(false);
    }
    IEnumerator AttackMelee3()
    {
        enemyMovement.SetSkill(true);
        animator.SetTrigger("canAttack3");

        yield return new WaitForSeconds(0.4f);

        enemyMovement.SetSkill(false);
    }
    private void OnDestroy()
    {
        player.GetComponent<PlayerSetUpgrades>().OpenCircleAttack();
        GameObject coinGameObject = Instantiate(coinPrefab,transform.position,Quaternion.identity);
        coinGameObject.GetComponent<Coin>().SetCoinMultiplier(2);
        coinGameObject.transform.SetParent(null, false);
    }
}
