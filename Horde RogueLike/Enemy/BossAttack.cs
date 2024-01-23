using System.Collections;
using UnityEngine;

public class BossAttack : Enemy
{
    [SerializeField] GameObject SpellSkillPrefab,coinPrefab;
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
                StartCoroutine(SpawnSpellSkill());
                break;

            case 1:
                if (!enemyStopMovement.CheckDistance(4))
                {
                    return;
                }
                AttackMelee();
                break;

            case 2:
                if (enemyStopMovement.CheckDistance(4))
                {
                    return;
                }
                StartCoroutine(Teleport());
                break;

            case 3:
                break;

            default:
                break;
        }
    }

    IEnumerator Teleport()
    {
        animator.SetTrigger("canTeleport");

        enemyMovement.SetSkill(true);

        yield return new WaitForSeconds(1);

        enemyMovement.SetSkill(false);

        transform.position = player.position;
    }
    IEnumerator SpawnSpellSkill()
    {

        animator.SetTrigger("canCast");
        enemyMovement.SetSkill(true);

        yield return new WaitForSeconds(1f);

        enemyMovement.SetSkill(false);

        yield return new WaitForSeconds(1f);

        float random = 0;
        for (int i = 0; i < Random.Range(10,26); i++)
        {
            Vector2 randomPlayerTransform = new Vector2(player.transform.position.x + Random.Range(-random, random + 1), player.transform.position.y + Random.Range(-random, random + 1));
            GameObject spellSkillGameObject = Instantiate(SpellSkillPrefab, randomPlayerTransform, Quaternion.identity);
            Destroy(spellSkillGameObject, 3);
            if (random < 5)
            {
                random += 0.75f;
            }
        }
        
    }
    void AttackMelee()
    {
        animator.SetTrigger("canAttack");
    }
    private void OnDestroy()
    {
        GameObject coinGameObject = Instantiate(coinPrefab, transform);
        coinGameObject.GetComponent<Coin>().SetCoinMultiplier(14);
        coinGameObject.transform.SetParent(null, false);
    }
}
