using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyMovement enemyMovement;
    protected EnemyHealth enemyHealth;
    protected EnemyStopMovement enemyStopMovement;

    protected Transform player;

    static protected int enemyDamage = 10;

    protected void CheckPlayer()
    {
        if (player == null)
            player = enemyMovement.GetPlayer();
    }
}
