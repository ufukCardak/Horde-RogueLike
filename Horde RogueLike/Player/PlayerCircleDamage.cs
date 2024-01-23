using UnityEngine;

public class PlayerCircleDamage : Player
{
    private void Awake()
    {
        playerCircleDamage = this;
    }
    public void SetArea()
    {
        transform.localScale = new Vector3(transform.localScale.x + area * 2f, transform.localScale.y + area * 2f);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            int damage = dmg + GetCrit();
            if (damage != dmg)
            {
                damage = damage / 2;
                enemyHealth.TakeDamageCircle(damage, 1.5f, true);
            }
            else
            {
                damage = damage / 2;
                enemyHealth.TakeDamageCircle(damage, 1.5f, false);
            }

        }
    }
}
