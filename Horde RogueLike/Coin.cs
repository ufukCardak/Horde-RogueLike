using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int coin,coinMultiplier;

    private void Awake()
    {
        coin = Random.Range(5, 11);
    }

    public void SetCoinMultiplier(int coinMultiplier)
    {
        this.coinMultiplier = coinMultiplier;
    }

    public int GetCoin()
    {
        if (coinMultiplier != 0)
        {
            return coin * coinMultiplier;
        }
        return coin * Singleton.Instance.GetPlayerLevel();
    }
}
