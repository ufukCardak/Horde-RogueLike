using UnityEngine;

public class PlayerSlashDamage : Player
{
    Animator animator;

    public float GetAnimatorSpeed()
    {
        return animator.speed;
    }
    public void SetAnimatorSpeed(float speed)
    {
        animator.speed = speed;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (gameObject.name == "PlayerSlashDamage")
        {
            playerSlashDamage = this;
        }
        else
        {
            playerSlashDamage2 = this;
        } 
    }
    public void SetAttackSpeed(float newAttackspeed)
    {
        if (newAttackspeed == -1)
        {
            attackspeed = attackspeed * 2;
            animator.speed += attackspeed;
            return;
        }

        attackspeed += newAttackspeed / 100;
        animator.speed += attackspeed;

        if (playerSlashDamage2 != null)
        {
            playerSlashDamage2.SetAnimatorSpeed(animator.speed);
        }
    }
    public void SetArea()
    {
        transform.localScale = new Vector3(transform.localScale.x + area, transform.localScale.y + area);
    }

    public void AttackMutation(float speed)
    {
        animator.speed = speed;
        playerSlashDamage.animator.Play("SlashAnim", -1, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable == null)
        {
            return;
        }

        int damage = dmg + GetCrit();
        bool isCrit = false;

        if (damage != dmg)
        {
            isCrit = true;
        }


        PlayerDamage playerDamage = new PlayerDamage(damage,isCrit,"Slash");

        damagable.TakenDamage(playerDamage);*/

        
        if (collision.gameObject.tag == "EnemyRangeCollider")
        {
            EnemyHealth enemyHealth = collision.GetComponentInParent<EnemyHealth>();
            int damage = dmg + GetCrit();
            bool isCrit = false;

            if (damage != dmg)
            {
                isCrit = true;
            }

            enemyHealth.TakeDamage(damage, isCrit, "Slash");

        }
        else if (collision.gameObject.tag == "Destroyable")
        {
            dropItem.CreateItem(collision.transform);
            Destroy(collision.gameObject);
        }
    }
}
