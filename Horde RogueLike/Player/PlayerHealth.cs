using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Player
{
    [SerializeField] Slider healthSlider;
    [SerializeField] int health;
    [SerializeField] int armor = 0;
    [SerializeField] int healthRegen;
    [SerializeField] int shieldTime;
    [SerializeField] GameObject gameOverPanel,pauseButton;
    bool canRegen = true;
    int regen = 1;
    bool shield = true;
    bool shieldMutation;

    [SerializeField] Material outlineMaterial,defaultMaterial;

    [SerializeField] TextMeshProUGUI textCoin;
    PlayerExp playerExp;

    bool isColliding;
    private void Awake()
    {
        defaultMaterial = transform.GetChild(1).GetComponent<SpriteRenderer>().material;
        playerExp = GetComponent<PlayerExp>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
        playerHealth = this;
        transform.GetChild(1).GetComponent<SpriteRenderer>().material = outlineMaterial;

        textCoin.text = PlayerPrefs.GetInt("Coin").ToString();


    }
    public void TakeDamage(int damage)
    {
        if (shield)
        {
            shield = false;
            StartCoroutine(ShieldActive());
            return;
        }
        damage -= armor;
        if (damage < 0)
        {
            damage = 5;
        }
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            AddHealth(Random.Range(30,45) * playerExp.GetPlayerLevel() / 5);
        }
        else if (collision.gameObject.tag == "Coin")
        {
            if (isColliding) return;
            isColliding = true;
            Destroy(collision.gameObject);
            Debug.Log("11");
            AddCoin(collision.GetComponent<Coin>().GetCoin());
        }
    }

    IEnumerator ResetColliding()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }

    void Death()
    {
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void AddHealth(int heal)
    {
        if (health == healthSlider.maxValue)
            return;
        else if (health + heal > healthSlider.maxValue)
            heal = (int)healthSlider.maxValue - health;

        health += heal;
        healthSlider.value = health;
    }

    void AddCoin(int droppedCoin)
    {
        int coin = int.Parse(textCoin.text) + droppedCoin;
        Debug.Log(coin);
        PlayerPrefs.SetInt("Coin", coin);
        textCoin.text = coin.ToString();
    }

    public void MaximumHealth(int maxHeal)
    {
        if (maxHeal == -1)
        {
            maxHeal = (int)healthSlider.maxValue/2;
        }

        healthSlider.maxValue += maxHeal;
        AddHealth(maxHeal);
    }
    public void SetHealthRegen(int newHealthRegen)
    {
        if (newHealthRegen == -1)
        {
            healthRegen = 7;
            return;
        }
        healthRegen -= newHealthRegen;
        regen++;
    }

    public void SetShieldTime(int newShieldTime)
    {
        shieldTime -= newShieldTime;
    }
    public void MaximumArmor(int maxArmor)
    {
        if(maxArmor == -1)
        {
            maxArmor = armor * 2;
        }
        armor += maxArmor;
    }
    private void Update()
    {
        if (health < healthSlider.maxValue && canRegen) 
        {
            StartCoroutine(HealthRegen());
        }
    }

    IEnumerator HealthRegen()
    {
        canRegen = false;
        AddHealth(regen);
        yield return new WaitForSeconds(healthRegen);
        canRegen = true;
    }

    IEnumerator ShieldActive() 
    {
        transform.GetChild(1).GetComponent<SpriteRenderer>().material = defaultMaterial;
        yield return new WaitForSeconds(shieldTime);
        transform.GetChild(1).GetComponent<SpriteRenderer>().material = outlineMaterial;
        shield = true;
    }

    public void StartStun(float stunTime)
    {
        if (shieldMutation)
        {
            return;
        }
        StartCoroutine(Stun(stunTime));
    }

    IEnumerator Stun(float stunTime)
    {
        canMove = false;
        yield return new WaitForSeconds(stunTime);
        canMove = true;
    }
}
