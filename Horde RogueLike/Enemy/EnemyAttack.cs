using System.Collections;
using UnityEngine;

public class EnemyAttack : Enemy
{
    bool enemyAttackDelay = true;
    [SerializeField] int damageMultiplier;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && enemyAttackDelay)
        {
            StartCoroutine(AttackDelay());
            collision.GetComponent<PlayerHealth>().TakeDamage(enemyDamage * damageMultiplier);
        }
    }
    public void SetDamage(int damage)
    {
        enemyDamage = damage;
    }
    
    IEnumerator AttackDelay()
    {
        enemyAttackDelay = false;
        
        yield return new WaitForSeconds(1f);
        
        enemyAttackDelay = true;

    }
    private void Awake()
    {
        if(damageMultiplier == 0)
        {
            damageMultiplier = 1;
        }
    }
}