using System.Collections;
using UnityEngine;

public class Boss3Attack : Enemy
{
    [SerializeField] GameObject DashSkillPrefab, coinPrefab;
    Animator animator;

    float oldSpeed;

    BoxCollider2D dashCollider;


    private void Awake()
    {
        animator = transform.GetChild(1).GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStopMovement = GetComponent<EnemyStopMovement>();
        dashCollider = GetComponent<BoxCollider2D>();
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

    IEnumerator Dash()
    {
        CheckPlayer();

        Vector2 dashTransform = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y).normalized;

        GameObject dashSkill = Instantiate(DashSkillPrefab,transform.position,Quaternion.identity);

        if (dashTransform.x > 0)
            dashSkill.transform.Rotate(new Vector3(0,0,0));
        else
            dashSkill.transform.Rotate(new Vector3(0, 180, 0));

        dashSkill.transform.SetParent(null, false);
        
        Destroy(dashSkill,1);

        enemyMovement.SetSkill(true);

        yield return new WaitForSeconds(0.5f);


        dashCollider.size = new Vector2(dashCollider.size.x, 1.5f);
        dashCollider.offset = new Vector2(dashCollider.offset.x, -0.1344156f);
        enemyStopMovement.DashForce(dashTransform);

        yield return new WaitForSeconds(0.5f);

        dashCollider.size = new Vector2(dashCollider.size.x, 0.1213245f);
        dashCollider.offset = new Vector2(dashCollider.offset.x, -0.8232813f);

        enemyMovement.SetSkill(false);
    }

    void Attack()
    {
        CheckPlayer();
        int random = Random.Range(0,3);
        switch (random)
        {
            case 0:
                StartCoroutine(Dash());
                break;

            case 1:
                if (!enemyStopMovement.CheckDistance(4))
                {
                    return;
                }
                StartCoroutine(AttackMelee());
                break;

            case 2:
                break;

            default:
                break;
        }
    }
    IEnumerator AttackMelee()
    {
        enemyMovement.SetSkill(true);
        animator.SetTrigger("canAttack1");
        yield return new WaitForSeconds(1.5f);
        enemyMovement.SetSkill(false);
    }
    private void OnDestroy()
    {
        GameObject coinGameObject = Instantiate(coinPrefab, transform);
        coinGameObject.GetComponent<Coin>().SetCoinMultiplier(8);
        coinGameObject.transform.SetParent(null, false);
    }
}
