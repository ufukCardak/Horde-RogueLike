using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerExp playerExp;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetJoystick(FixedJoystick newJoystick)
    {
        playerMovement.SetJoystick(newJoystick);
    }

    public int GetPlayerLevel()
    {
        return playerExp.GetPlayerLevel();
    }
}
