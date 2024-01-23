using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Enemy,IDamagable
{
    [SerializeField] int health = 100;
    Transform expParent;
    
    public GameObject damageTextPrefab,expPrefab;
    Slider healthBar;
    SpriteRenderer spriteRenderer;
    bool canTakeDamageCircle = true;
    int exp,expIndex,armor;
    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();

        spriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }

    public void SetEnemyHealthExp(int health,int armor,int exp,bool isBoss,Transform expParent)
    {
        this.health = health;
        this.armor = armor;
        this.exp = exp;
        this.expParent = expParent;
        healthBar.maxValue = health;
        healthBar.value = health;

        expIndex = Random.Range(0, 2);

        if (isBoss)
        {
            healthBar.gameObject.SetActive(true);
            expIndex = Random.Range(2, 4);
        }
        //Debug.Log("enemyHealth " + enemyHealth + " exp " + exp);
    }

    public void SetEnemyHealth(int health)
    {
        this.health = health;
    }

    public int GetEnemyHealth()
    {
        return health;
    }
    public void TakeDamage(int dmg,bool crit,string attackType)
    {
        if (health > 0)
        {
            GameObject dmgTextGameobject = Instantiate(damageTextPrefab, transform);
            dmgTextGameobject.transform.SetParent(null, false);
            dmgTextGameobject.transform.position = gameObject.transform.position;
            TextMeshProUGUI dmgText = dmgTextGameobject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            dmg -= armor;

            if (dmg <= 0)
            {
                dmg = 5;
            }

            dmgText.text = dmg.ToString();
            if (crit)
            {
                dmgText.color = Color.yellow;
            }
            health -= dmg;
            healthBar.value = health;
            if (attackType == "Slash")
            {
                StartCoroutine(CanTakeDamageWait());
            }
        }

        if (health <= 0)
        {
            EnemyDeath();
        }
    }
    public void TakeDamageCircle(int dmg,float dmgTime,bool crit)
    {
        
        if (!canTakeDamageCircle) 
        {
            return;
        }
        StartCoroutine(CanTakeDamageWaitCircle(dmgTime));
        TakeDamage(dmg,crit,"Circle");
    }

    IEnumerator CanTakeDamageWaitCircle(float dmgTime)
    {

        canTakeDamageCircle = false;

        StartCoroutine(CanTakeDamageWait());

        yield return new WaitForSeconds(dmgTime);
        
        canTakeDamageCircle = true;
        
    }

    IEnumerator CanTakeDamageWait()
    {
        enemyMovement.SetStun(true);
        spriteRenderer.color = Color.black;

        yield return new WaitForSeconds(0.25f);

        enemyMovement.SetStun(false);
        spriteRenderer.color = Color.white;
    }

    void EnemyDeath()
    {
        if (expParent == null)
        {
            Destroy(gameObject);
            return;
        }

        GameObject expGameObject = Instantiate(expPrefab, transform);
        expGameObject.transform.SetParent(expParent);
        expGameObject.transform.position = gameObject.transform.position;
        expGameObject.GetComponent<Exp>().SetExp(exp, expIndex);

        Destroy(gameObject);
    }

    public void TakenDamage(PlayerDamage playerDamage)
    {
        TakeDamage(playerDamage.Damage, playerDamage.IsCrit, playerDamage.AttackType);
    }
}
