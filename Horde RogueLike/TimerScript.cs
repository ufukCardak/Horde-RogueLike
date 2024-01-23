using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    TextMeshProUGUI timerText;
    [SerializeField] EnemySpawn enemySpawn;
    [SerializeField] float sec;
    [SerializeField] int min,bossTier;

    // Start is called before the first frame update
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        if (sec >= 60)
        {
            sec = 0;
            min++;
            CalculateHealth();
            enemySpawn.SetSpawnTimer(5/min);


            if (min >= 7 && bossTier == 0)
            {
                bossTier++;
                enemySpawn.SpawnBigBoss(bossTier);
            }else if (min >= 13 && bossTier == 1)
            {
                bossTier++;
                enemySpawn.SpawnBigBoss(bossTier);
            }
            else if (min >= 16 && bossTier == 2)
            {
                bossTier++;
                enemySpawn.SpawnBigBoss(bossTier);
            }
            else if (min >= 19 && bossTier == 3)
            {
                bossTier++;
                enemySpawn.SpawnBigBoss(bossTier);
            }else if(min >= 24 && bossTier == 4 && min % 2 == 0)
            {
                for (int i = 0; i < Random.Range(2,5); i++)
                {
                    int random = Random.Range(0, 4);
                    enemySpawn.SpawnBigBoss(random);
                }
                
            }

        }

        timerText.text = min + ":" + sec.ToString("#");
    }
    void CalculateHealth()
    {
        int enemyHealth = 10;
        int count = min;

        while (count % 1 == 0 && count != 0)
        {
            enemyHealth += Random.Range(10, 21);
            count -= 1;
        }

        Debug.Log("enemyhealth " + enemyHealth);

        enemySpawn.SetEnemyHealth(enemyHealth);
    }
    public int GetMin()
    {
        return min;
    }

}
