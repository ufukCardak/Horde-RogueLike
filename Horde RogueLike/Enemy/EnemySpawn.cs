using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Material outlineMaterial;
    [SerializeField] Transform expParent;

    public List<GameObject> bossList;
    float timer;
    public int multiplierCalculate = 100;

    PlayerExp playerExp;

    int enemyHealth = 10;

    int enemyDamage = 70;

    [SerializeField] int enemyCount,enemyRangeCount;
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f && enemyCount <= 100)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }
    public void SetSpawnTimer(int spawnTimer)
    {
        switch (spawnTimer)
        {
            case 5:
                this.spawnTimer = 5;
                break;

            case 4:
                this.spawnTimer = 4;
                break;

            case 3:
                this.spawnTimer = 3;
                break;

            case 2:
                this.spawnTimer = 2;
                break;

            case 1:
                this.spawnTimer = 1;
                break;

            default:
                break;
        }
    }

    void SpawnMultipleEnemys()
    {
        for (int i = 0; i < Random.Range(5,11) * 5 / spawnTimer; i++)
        {
            SpawnEnemy();
        }
    }
    public void SetEnemyCount()
    {
        if (enemyCount <= 0)
        {
            return;
        }
        enemyCount--;
    }
    public void SetEnemyRangeCount()
    {
        if(enemyRangeCount <= 0)
        {
            return;
        }
        enemyRangeCount--;
        enemyCount--;
    }
    private void Start()
    {
        playerExp = player.GetComponent<PlayerExp>();

        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }
        //SpawnWizard();
        //SpawnBoss();

        //Invoke("SpawnMultipleEnemys",10);
        Invoke("SpawnBoss", Random.Range(15, 26));
        Invoke("SpawnWizard", Random.Range(100, 181));
    }

    public void SpawnBigBoss(int index)
    {
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;

        GameObject bigBoss = Instantiate(bossList[index],position,Quaternion.identity);
        bigBoss.GetComponent<EnemyMovement>().SetPlayer(player);
        bigBoss.GetComponent<EnemyHealth>().SetEnemyHealthExp(Random.Range(enemyHealth * 35, enemyHealth * 40),Random.Range(0,5) * Singleton.Instance.GetPlayerLevel(), CalculateExp(1), true, expParent);
    }

    public void RemoveEnemyBoss(bool isWizard)
    {
        if (!isWizard)
        {
            Invoke("SpawnBoss", Random.Range(15, 26));
            Invoke("SpawnMultipleEnemys", Random.Range(9, 15));
        }
        else
        {
            Invoke("SpawnWizard", Random.Range(100, 181));
        }

    }

    void SpawnWizard()
    {
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;

        GameObject wizardGameObject = Instantiate(bossList[0],position,Quaternion.identity);

        wizardGameObject.GetComponent<EnemyMovement>().SetPlayer(player);
        wizardGameObject.GetComponent<EnemyHealth>().SetEnemyHealthExp(Random.Range(enemyHealth * 10, enemyHealth * 16),Random.Range(3, 7) * Singleton.Instance.GetPlayerLevel(), CalculateExp(4), true, expParent);

        wizardGameObject.AddComponent<EnemyBossScript>().SetEnemySpawn(this,true);
    }

    int RandomEnemyPrefab()
    {
        int randomPrefab = Random.Range(0, enemyPrefabs.Length);
        int rangeSpawnChance = Random.Range(0, 100);

        if (randomPrefab >= 5 && enemyRangeCount >= 2)
        {
            return RandomEnemyPrefab();
        }
        else
        {
            if (rangeSpawnChance > 25 && randomPrefab >= 5)
            {
                return RandomEnemyPrefab();
            }
            else
            {
                return randomPrefab;
            }
        }  
    }

    private void SpawnEnemy()
    {
        enemyCount++;
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;
        int enemyType = RandomEnemyPrefab();

        GameObject newEnemy = Instantiate(enemyPrefabs[enemyType]);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<EnemyMovement>().SetPlayer(player);
        newEnemy.GetComponent<EnemyAttack>().SetDamage(enemyDamage);

        int armorMultiplier = Random.Range(0, 4) * (Singleton.Instance.GetPlayerLevel() / 4);

        if (armorMultiplier == 0)
        {
            armorMultiplier = 1;
        }


        newEnemy.GetComponent<EnemyHealth>().SetEnemyHealthExp(Random.Range(enemyHealth / 2, enemyHealth * 2 + 1), Random.Range(0, 4) * armorMultiplier, CalculateExp(10), false, expParent);

        if (enemyType < 5)
        {
            newEnemy.AddComponent<EnemyCount>().SetEnemySpawn(this);
        }
        else
        {
            enemyRangeCount++;
            newEnemy.GetComponent<EnemyAttackRange>().SetEnemySpawn(this);
        }
    }
    private void SpawnBoss()
    {
        Vector3 position = GenerateRandomPosition();

        position += player.transform.position;

        int enemyType = Random.Range(0, enemyPrefabs.Length);

        GameObject bossEnemy = Instantiate(enemyPrefabs[enemyType]);
        bossEnemy.transform.position = position;
        bossEnemy.GetComponent<EnemyMovement>().SetPlayer(player);
        bossEnemy.GetComponent<EnemyHealth>().SetEnemyHealthExp(Random.Range(enemyHealth * 5, enemyHealth * 10), Random.Range(5, 9) * Singleton.Instance.GetPlayerLevel(), CalculateExp(4),true, expParent);
        bossEnemy.transform.GetChild(1).GetComponent<SpriteRenderer>().material = outlineMaterial;
        bossEnemy.AddComponent<EnemyBossScript>().SetEnemySpawn(this,false);
    }


    public void SetEnemyHealth(int enemyHealth)
    {
        enemyDamage += 20;
        this.enemyHealth = enemyHealth;
    }

    int CalculateExp(int div)
    {
        return Calculate(div);
    }
    int Calculate(int div)
    {
        int playerLevel = playerExp.GetPlayerLevel();
        int count = 0;

        for (int i = 0; i <= playerLevel; i++)
        {
            count += (int)Mathf.Floor(i + multiplierCalculate * Mathf.Pow(2, i / 7));
        }
        return count / div;
    }
    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();
        float f = Random.value > 0.5f ? -1f : 1f;
        if (Random.value > 0.5f)
        {
            position.x = Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }

        position.z = 0;

        return position;
    }
}
