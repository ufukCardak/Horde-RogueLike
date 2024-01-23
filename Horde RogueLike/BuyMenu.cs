using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenu : MonoBehaviour
{
    [SerializeField] int[] buyCountList;

    [SerializeField] static Button[] buyButtonsList;
    [SerializeField] static Slider[] buySliders;
    [SerializeField] TextMeshProUGUI[] nameTexts;

    [SerializeField] int coin;

    [SerializeField] TextMeshProUGUI coinText;

    void SetBuyCount(int buyIndex)
    {
        string buyedUpgrades = "";
        for (int i = 0; i < buyCountList.Length; i++) 
        {
            if (buyIndex != buyCountList[i])
            {
                buyedUpgrades += buyCountList[i];
                buyedUpgrades += "-";
            }
            else
            {
                buyedUpgrades += buyIndex;
                buyedUpgrades += "-";
            }
        }

        PlayerPrefs.SetString("buyedUpgrades", buyedUpgrades);

        BuyCountStringToList();
    }

    public void BuyCountStringToList()
    {
        string[] convertedString = new string[buyCountList.Length];
        string buyedUpgrades = PlayerPrefs.GetString("buyedUpgrades");

        if (buyedUpgrades == "")
        {
            return;
        }

        convertedString = buyedUpgrades.Split("-");

        for (int i = 0;i < buyCountList.Length;i++)
        {
            buyCountList[i] = int.Parse(convertedString[i]);
        }

    }

    private void Awake()
    {

        coin = PlayerPrefs.GetInt("Coin");
        buyButtonsList = transform.GetChild(0).GetComponentsInChildren<Button>();
        buyCountList = new int[buyButtonsList.Length];
        buySliders = new Slider[buyButtonsList.Length];
        nameTexts = new TextMeshProUGUI[buyButtonsList.Length];

        
        BuyCountStringToList();
        CheckAllCount();

        coinText.text = coin.ToString();
        if (PlayerPrefs.GetString("language") == "ENG" || PlayerPrefs.GetString("language") == "")
        {
            CheckLanguage();
        }
    }
    void CheckLanguage()
    {
        for (int i = 0; i < nameTexts.Length; i++)
        {
            nameTexts[i] = buyButtonsList[i].transform.GetChild(4).GetComponent<TextMeshProUGUI>();

            switch (buyButtonsList[i].transform.GetChild(3).name)
            {
                case "Damage":
                    nameTexts[i].text = "Damage";
                    break;

                case "Health":
                    nameTexts[i].text = "Health";
                    break;

                case "Armor":
                    nameTexts[i].text = "Armor";
                    break;

                case "Regen":
                    nameTexts[i].text = "Regen";
                    break;

                case "Speed":
                    nameTexts[i].text = "Speed";
                    break;

                case "Area":
                    nameTexts[i].text = "Attack Area";
                    break;

                case "CritChance":
                    nameTexts[i].text = "Crit Chance";
                    break;

                case "CritDamage":
                    nameTexts[i].text = "Crit Damage";
                    break;

                case "Shield":
                    nameTexts[i].text = "Shield";
                    break;

                case "AttackSpeed":
                    nameTexts[i].text = "Attack Speed";
                    break;

                case "Collection":
                    nameTexts[i].text = "Collection Area";
                    break;

                case "ExpMultiplier":
                    nameTexts[i].text = "Exp";
                    break;

                default:
                    break;
            }
        }
    }

    void CheckAllCount()
    {
        for (int i = 0; i < buyButtonsList.Length; i++)
        {
            int price = (int) Math.Pow(buyCountList[i] + 1, 2) * 100;
            if (buyCountList[i] >= 0 && buyCountList[i] < 5 && coin >= price  && coin > 0)
            {
                buyButtonsList[i].interactable = true;
                buyButtonsList[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                buyButtonsList[i].interactable = false;
                buyButtonsList[i].transform.GetChild(1).gameObject.SetActive(false);
            }

            buySliders[i] = buyButtonsList[i].transform.GetChild(0).GetComponent<Slider>();
            buySliders[i].value = buyCountList[i] * 25;

            if (price <= 2500)
            {
                buyButtonsList[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = price.ToString();
            }
            else
            {
                buyButtonsList[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void SetCoin(int coin)
    {
        this.coin = coin;
        PlayerPrefs.SetInt("Coin",coin);
        coinText.text = this.coin.ToString();
    }

    void CheckCount(int buyIndex)
    {
        buyCountList[buyIndex]++;
        int price = (int)(Math.Pow(buyCountList[buyIndex], 2) * 100);
        if (coin < price)
        {
            buyCountList[buyIndex]--;
            return;
        }
        coin -= price;
        SetCoin(coin);
        SetBuyCount(buyIndex);
    }

    public void GetBuyString(string buyName)
    {
        Debug.Log(buyName);

        switch (buyName)
        {
            case "Damage":
                CheckCount(0);
                break;

            case "CritChance":
                CheckCount(1);
                break;

            case "CritDamage":
                CheckCount(2);
                break;

            case "AttackSpeed":
                CheckCount(3);
                break;

            case "Armor":
                CheckCount(4);
                break;

            case "Area":
                CheckCount(5);
                break;

            case "Collection":
                CheckCount(6);
                break;

            case "ExpMultiplier":
                CheckCount(7);
                break;

            case "Health":
                CheckCount(8);
                break;

            case "Regen":
                CheckCount(9);
                break;

            case "Shield":
                CheckCount(10);
                break;

            case "Speed":
                CheckCount(11);
                break;

            default:
                break;
        }
        CheckAllCount();
    }
}
