using UnityEngine;

public class Player : MonoBehaviour
{
    protected static PlayerSetUpgrades setUpgrades;
    protected static PlayerHealth playerHealth;
    protected static PlayerMovement playerMovement;
    protected static PlayerSlashDamage playerSlashDamage;
    protected static PlayerSlashDamage playerSlashDamage2;
    protected static PlayerCircleDamage playerCircleDamage;
    protected static DropItem dropItem;


    protected static int dmg = 10;
    protected static int critChance = 1;
    protected static int critDamage = 0;
    protected static float expMultiplier = 0;
    protected static float area = 0;
    protected static float attackspeed = 0;

    protected static bool canMove = true;
    protected int GetCrit()
    {
        int random = Random.Range(0, 101);
        if (critChance >= random)
        {
            return critDamage;
        }
        return 0;
    }
}
