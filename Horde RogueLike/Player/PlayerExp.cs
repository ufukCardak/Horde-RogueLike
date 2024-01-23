using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExp : Player
{
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] float playerExp;
    [SerializeField] int playerLevel = 1;

    [SerializeField] List<UpgradeData> upgrades;
    [SerializeField] List<UpgradeData> mutations;
    [SerializeField] UpgradePanel upgradePanel;

    List<UpgradeData> selectedUpgradesMutations;

    [SerializeField] List<UpgradeData> acquiredUpgrades;

    public int damageLevel;

    [SerializeField] TimerScript timerScript;

    private void Awake()
    {
        expSlider.value = playerExp;
        expSlider.gameObject.SetActive(true);
        levelText.text = playerLevel.ToString();

        

        Time.timeScale = 1f;
    }

    private void Start()
    {
        SetBuyUpgrades();
    }
    public int GetPlayerLevel()
    {
        return playerLevel;
    }
    public void SetExpMultiplier(float expMulti)
    {
        if (expMulti == -1)
        {
            expMultiplier += 100;
        }
        expMultiplier += expMulti;
    }
    public void AddExp(float exp)
    {
        exp += exp * expMultiplier / 100;
        playerExp += exp;
        if (playerExp >= expSlider.maxValue)
        {
            LevelUp();
        }
        expSlider.value = playerExp;
    }

    public void Mutation(int selectedMutation)
    {
        UpgradeData upgradeData = selectedUpgradesMutations[selectedMutation];
        switch (upgradeData.upgradeName)
        {
            case "Damage":
                setUpgrades.SetDmg(-1);
                damageLevel++;
                break;

            case "Health":
                playerHealth.MaximumHealth(-1);
                break;

            case "Regen":
                playerHealth.SetHealthRegen(-1);
                break;

            case "Armor":
                playerHealth.MaximumArmor(-1);
                break;

            case "Area":
                setUpgrades.SetArea(-1);
                break;

            case "CritChance":
                setUpgrades.SetCritChance(-1);
                break;

            case "CritDamage":
                setUpgrades.SetCritDamage(-1);
                break;

            case "AttackSpeed":
                setUpgrades.SetAttackSpeed(-1);
                break;

            case "Collection":
                setUpgrades.SetCollectionArea(-1);
                break;

            case "ExpMultiplier":
                SetExpMultiplier(-1);
                break;

            default:
                break;
        }
        if (upgradeData.mutation == 1)
        {
            mutations.Remove(upgradeData);
            acquiredUpgrades.Add(upgradeData);
        }
    }

    public void Upgrade(int selectedUpgrade)
    {
        UpgradeData upgradeData = selectedUpgradesMutations[selectedUpgrade];

        upgradeData.upgradeCurrentLevel += 1;

        if (upgradeData.mutation == 1)
        {
            Mutation(selectedUpgrade);
            return;
        }
        else if (upgradeData.upgradeCurrentLevel >= 5 && upgradeData.mutation == -1)
        {
            upgrades.Remove(upgradeData);
        }
        else if (upgradeData.upgradeCurrentLevel >= 5 && upgradeData.mutation == 0 )
        {
            upgrades.Remove(upgradeData);
            mutations.Add(upgradeData);
            upgradeData.mutation += 1;
        }


        switch (upgradeData.upgradeName)
        {
            case "Damage":
                setUpgrades.SetDmg(upgradeData.upgradeNextValue);

                switch (damageLevel)
                {
                    case 0:
                        upgradeData.upgradeNextValue = 26;
                        break;

                    case 1:
                        upgradeData.upgradeNextValue = 45;
                        break;

                    case 2:
                        upgradeData.upgradeNextValue = 71;
                        break;

                    case 3:
                        upgradeData.upgradeNextValue = 119;
                        break;

                    default:
                        break;
                }
                damageLevel++;
                break;

            case "Health":
                playerHealth.MaximumHealth(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(30, 46);
                break;

            case "Regen":
                playerHealth.SetHealthRegen(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(1, 3);
                break;

            case "Armor":
                playerHealth.MaximumArmor(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(3, 7);
                break;

            case "Speed":
                playerMovement.SetSpeed(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(1, 3);
                break;

            case "Area":
                setUpgrades.SetArea(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(1, 3);
                break;

            case "CritChance":
                setUpgrades.SetCritChance(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(1, 4);
                break;

            case "CritDamage":
                setUpgrades.SetCritDamage(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(2, 7);
                break;

            case "Shield":
                playerHealth.SetShieldTime(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(1, 3);
                break;

            case "AttackSpeed":
                setUpgrades.SetAttackSpeed(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue++;
                break;

            case "Collection":
                setUpgrades.SetCollectionArea(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(1, 4);
                break;

            case "ExpMultiplier":
                SetExpMultiplier(upgradeData.upgradeNextValue);
                upgradeData.upgradeNextValue += Random.Range(2, 6);
                break;

            default:

                break;
        }
    }

    private void SetBuyUpgrades()
    {
        string buyedUpgrades = PlayerPrefs.GetString("buyedUpgrades");

        if (buyedUpgrades == "")
        {
            return;
        }
        int[] buyedUpgradesList = new int[12];
        string[] convertedString = new string[buyedUpgradesList.Length];

        convertedString = buyedUpgrades.Split("-");

        for (int i = 0; i < buyedUpgradesList.Length; i++)
        {
            buyedUpgradesList[i] = int.Parse(convertedString[i]);
            switch (i)
            {
                case 0:
                    setUpgrades.SetDmg(buyedUpgradesList[i] * 2);
                    break;

                case 1:
                    setUpgrades.SetCritChance(buyedUpgradesList[i] * 3);
                    break;

                case 2:
                    setUpgrades.SetCritDamage(buyedUpgradesList[i] * 5);
                    break;

                case 3:
                    setUpgrades.SetAttackSpeed(buyedUpgradesList[i]);
                    break;

                case 4:
                    playerHealth.MaximumArmor(buyedUpgradesList[i] * 3);
                    break;

                case 5:
                    setUpgrades.SetArea(buyedUpgradesList[i]);
                    break;

                case 6:
                    setUpgrades.SetCollectionArea(buyedUpgradesList[i] * 2);
                    break;

                case 7:
                    SetExpMultiplier(buyedUpgradesList[i]);
                    break;

                case 8:
                    playerHealth.MaximumHealth(buyedUpgradesList[i] * 25);
                    break;

                case 9:
                    playerHealth.SetHealthRegen(buyedUpgradesList[i]);
                    break;

                case 10:
                    playerHealth.SetShieldTime(buyedUpgradesList[i]);
                    break;

                case 11:
                    playerMovement.SetSpeed(buyedUpgradesList[i]);
                    break;
            }
        }
    }

    private void LevelUp()
    {
        int selected = 4;


        if (selectedUpgradesMutations == null)
        {
            selectedUpgradesMutations = new List<UpgradeData>();
        }

        selectedUpgradesMutations.Clear();

        if (mutations.Count < 2)
        {
            selectedUpgradesMutations.AddRange(GetMutations(mutations.Count));
        }
        else
        {
            selectedUpgradesMutations.AddRange(GetMutations(2));
        }

        if (selectedUpgradesMutations.Count > 0)
        {
            selected -= selectedUpgradesMutations.Count;
        }



        selectedUpgradesMutations.AddRange(GetUpgrades(selected));

        if (selectedUpgradesMutations.Count > 0)
        {
            upgradePanel.OpenPanel(selectedUpgradesMutations);
        }

        playerExp -= expSlider.maxValue;
        playerLevel++;
        expSlider.maxValue = CalculateRequiredExp(50,4);
        levelText.text = playerLevel.ToString();
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();
        List<UpgradeData> selectRandomUpgradeList = new List<UpgradeData>(upgrades);

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }
        
        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, selectRandomUpgradeList.Count);

            if (selectRandomUpgradeList[random].upgradeName == "Damage" && timerScript.GetMin() <= 3 * selectRandomUpgradeList[random].upgradeCurrentLevel)
            {

                if (count == upgrades.Count && i + 1 == count)
                {
                    Debug.Log("1");
                    return upgradeList;
                }
                else
                {
                    Debug.Log("3");
                    i--;
                }
                
            }
            else
            {
                upgradeList.Add(selectRandomUpgradeList[random]);
                selectRandomUpgradeList.Remove(selectRandomUpgradeList[random]);
            }

        }

        return upgradeList;
    }
    public List<UpgradeData> GetMutations(int count)
    {
        List<UpgradeData> mutationList = new List<UpgradeData>();
        List<UpgradeData> selectRandomMutationList = new List<UpgradeData>(mutations);

        if (count > mutations.Count)
        {
            count = mutations.Count;
        }

        for (int i = 0; i < count; i++)
        {
            int random = Random.Range(0, selectRandomMutationList.Count);
            mutationList.Add(selectRandomMutationList[random]);
            selectRandomMutationList.Remove(selectRandomMutationList[random]);
        }

        return mutationList;
    }
    int CalculateRequiredExp(int multiplier,int div)
    {
        int requiredExp = 0;

        for (int i = 0; i <= playerLevel; i++)
        {
            requiredExp += (int)Mathf.Floor(i + multiplier * Mathf.Pow(2, i / div));
        }
        Debug.Log("exp " + requiredExp);
        return requiredExp / 2;
    }
}
