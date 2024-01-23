using UnityEngine;

public class PlayerSetUpgrades : Player
{
    [SerializeField] GameObject otherAttack,circleAtack;
    [SerializeField] Exp exp;
    [SerializeField] ExpParent expParent;
    private void Awake()
    {
        setUpgrades = this;
    }
    public void SetAttackSpeed(float attackspeed)
    {
        playerSlashDamage.SetAttackSpeed(attackspeed);
    }
    public void SetDmg(int newDmg)
    {
        if (newDmg == -1)
        {
            dmg += dmg * 50 / 100;
        }
        else
        {
            //dmg += dmg * newDmg / 100;
            dmg += newDmg;
        }
    }    
    public void SetArea(float newArea)
    {
        if (newArea == -1)
        {
            SlashMutation();
            return;
        }

        area += newArea/100;
        if (playerSlashDamage != null)
            playerSlashDamage.SetArea();

        if (playerCircleDamage != null)
            playerCircleDamage.SetArea();
    }

    public void SetCritChance(int newCritChance)
    {
        if (newCritChance == -1)
        {
            newCritChance = critChance / 2;
        }
        critChance += newCritChance;
    }
    
    public void SetCritDamage(int newCritDamage)
    {
        if (newCritDamage == -1)
        {
            newCritDamage += critDamage;
        }
        critDamage += newCritDamage;
    }
    void SlashMutation()
    {
        if (playerSlashDamage != null)
        {
            otherAttack.SetActive(true);
            otherAttack.transform.localScale = playerSlashDamage.transform.localScale;
            playerSlashDamage2.AttackMutation(playerSlashDamage.GetAnimatorSpeed());
        }

    }
    public void SetCollectionArea(float area)
    {
        if (area == -1)
        {
            expParent.ExpMutation();
            return;
        }
        area = area / 100;
        exp.SetCollectionArea(area);
    }
    
    public void OpenCircleAttack()
    {
        circleAtack.SetActive(true);
        playerCircleDamage.SetArea();
    }
}
