using UnityEngine;

public class EnemyNewPosition : MonoBehaviour
{
    Vector2 spawnArea = new Vector2(10, 8);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GenerateRandomPosition(transform.name);

            collision.transform.position = GenerateRandomPosition(transform.name) + transform.parent.position;
        }
    }
    private Vector3 GenerateRandomPosition(string direction)
    {
        Vector3 position = new Vector3();

        position.y = Random.Range(-spawnArea.y, spawnArea.y);
        position.x = spawnArea.x;

        switch (direction)
        {
            case "Up":
                position.x = Random.Range(-spawnArea.x, spawnArea.x);
                position.y = -spawnArea.y;
                break;

            case "Down":
                position.x = Random.Range(-spawnArea.x, spawnArea.x);
                position.y = spawnArea.y;
                break;

            case "Left":
                position.y = Random.Range(-spawnArea.y, spawnArea.y);
                position.x = spawnArea.x;
                break;

            case "Right":
                position.y = Random.Range(-spawnArea.y, spawnArea.y);
                position.x = -spawnArea.x;
                break;

            default:
                position.y = Random.Range(-spawnArea.y, spawnArea.y);
                position.x = Random.Range(-spawnArea.x, spawnArea.y);
                break;
        }


        position.z = 0;

        return position;
    }
}
