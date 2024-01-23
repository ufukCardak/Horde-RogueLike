using UnityEngine;

public class EnemyStopMovement : Enemy
{
    [SerializeField] float distance;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        player = enemyMovement.GetPlayer();
    }
    private void Update()
    {
        distance = Vector2.Distance(player.position, transform.position);

        if (enemyMovement.GetAway())
        {
            return;
        }
    }

    public float GetDistance()
    {
        return distance;
    }


    public void MoveAwayFromPlayer()
    {
        if (player == null)
        {
            return;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        enemyMovement.GetRb().velocity = new Vector2(-direction.x, direction.y + Random.Range(0, 1)) * enemyMovement.GetSpeed();
    }

    public void DashForce(Vector2 dashTransform)
    {
        enemyMovement.GetRb().AddForce(new Vector2(dashTransform.x, dashTransform.y) * 750);
    }
    public bool CheckDistance(float skillStopDistance)
    {
        return distance <= skillStopDistance;
    }
}
