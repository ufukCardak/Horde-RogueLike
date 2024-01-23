using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    string language;
    [SerializeField] string Name;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI explanation;
    [SerializeField] int nextValue;
    private void Awake()
    {
        language = PlayerPrefs.GetString("language");
        if (language == "")
        {
            language = "ENG";
        }
    }

    public void SetMutationButton(UpgradeData upgradeData)
    {
        Name = upgradeData.upgradeName;
        icon.sprite = upgradeData.upgradeIcon;
        nextValue = upgradeData.upgradeNextValue;

        if (language == "TR")
        {
            switch (Name)
            {
                case "Damage":
                    explanation.text = "Hasarý %50 artýr";
                    break;

                case "Health":
                    explanation.text = "Canýný %50 artýr";
                    break;

                case "Armor":
                    explanation.text = "Zýrhýný 2 kat artýr";
                    break;

                case "Regen":
                    explanation.text = "Can Yenileme artýk 7 saniye";
                    break;

                case "Area":
                    explanation.text = "Saldýrýný Çoðaltýr";
                    break;

                case "CritChance":
                    explanation.text = "Kritik þansini %50 artýr";
                    break;

                case "CritDamage":
                    explanation.text = "Kritik hasarýný %100 artýr";
                    break;

                case "AttackSpeed":
                    explanation.text = "Saldýrý hýzý %25 artýr";
                    break;

                case "Collection":
                    explanation.text = "35 saniyede bir tüm eþyalarý topla";
                    break;

                case "ExpMultiplier":
                    explanation.text = "%100 daha fazla exp kazan";
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (Name)
            {
                case "Damage":
                    explanation.text = "Increase damage by 50%";
                    break;

                case "Health":
                    explanation.text = "Increase your health by 50%";
                    break;

                case "Armor":
                    explanation.text = "Increase your armor by 2x";
                    break;

                case "Regen":
                    explanation.text = "Health Regen is now 7 seconds";
                    break;

                case "Area":
                    explanation.text = "Multiplies Your Attack";
                    break;

                case "CritChance":
                    explanation.text = "Increase critical chance by 50%";
                    break;

                case "CritDamage":
                    explanation.text = "Increase critical damage by %50";
                    break;

                case "AttackSpeed":
                    explanation.text = "Increase attack speed by 25%";
                    break;

                case "Collection":
                    explanation.text = "Collect all items every 35 seconds";
                    break;
                    
                case "ExpMultiplier":
                    explanation.text = "Earn 100% more exp";
                    break;

                default:
                    return;
            }
        }
        
    }
    public void SetUpgradeButton(UpgradeData upgradeData)
    {
        Name = upgradeData.upgradeName;
        icon.sprite = upgradeData.upgradeIcon;
        nextValue = upgradeData.upgradeNextValue;

        if (upgradeData.mutation == 1)
        {
            SetMutationButton(upgradeData);
            return;
        }

        if (language == "TR")
        {
            switch (Name)
            {
                case "Damage":
                    explanation.text = "+" + nextValue + " Hasar";
                    break;

                case "Health":
                    explanation.text = "+" + nextValue + " Can";
                    break;

                case "Armor":
                    explanation.text = "+" + nextValue + " Zýrh";
                    break;

                case "Regen":
                    explanation.text = "+" + nextValue + " Can Yenileme";
                    break;

                case "Speed":
                    explanation.text = "%" + nextValue + " Hýz";
                    break;

                case "Area":
                    explanation.text = "%" + nextValue + " Saldýrý Alaný";
                    break;

                case "CritChance":
                    explanation.text = "%" + nextValue + " Kritik Þansý";
                    break;

                case "CritDamage":
                    explanation.text = "+" + nextValue + " Kritik Hasarý";
                    break;

                case "Shield":
                    explanation.text = "-" + nextValue + " Kalkan Süresi";
                    break;

                case "AttackSpeed":
                    explanation.text = "%" + nextValue + " Saldýrý Hýzý";
                    break;

                case "Collection":
                    explanation.text = "%" + nextValue + " Toplama Alaný";
                    break;

                case "ExpMultiplier":
                    explanation.text = "%" + nextValue + " Daha Hýzlý Level Atlama";
                    break;

                default:
                    break;
            }
        }
        else
        {
            switch (Name)
            {
                case "Damage":
                    explanation.text = "+" + nextValue + " Damage";
                    break;

                case "Health":
                    explanation.text = "+" + nextValue + " Health";
                    break;

                case "Armor":
                    explanation.text = "+" + nextValue + " Armor";
                    break;

                case "Regen":
                    explanation.text = "+" + nextValue + " Health Regen";
                    break;

                case "Speed":
                    explanation.text = "%" + nextValue + " Speed";
                    break;

                case "Area":
                    explanation.text = "%" + nextValue + " Attack Area";
                    break;

                case "CritChance":
                    explanation.text = "%" + nextValue + " Critical Chance";
                    break;

                case "CritDamage":
                    explanation.text = "+" + nextValue + " Critical Damage";
                    break;

                case "Shield":
                    explanation.text = "-" + nextValue + " Shield Duration";
                    break;

                case "AttackSpeed":
                    explanation.text = "%" + nextValue + " Attack Speed";
                    break;

                case "Collection":
                    explanation.text = "%" + nextValue + " Collection Area";
                    break;

                case "ExpMultiplier":
                    explanation.text = "%" + nextValue + " Exp";
                    break;

                default:
                    break;
            }
        }
    }

    public void Clean()
    {
        Name = "";
        icon.sprite = null;
        explanation.text = "";
        nextValue = 0;
    }
}
