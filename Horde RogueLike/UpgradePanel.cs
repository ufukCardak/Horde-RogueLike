using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] PlayerExp player;


    private void Start()
    {
        GetComponent<LoadScene>().FirstCheck();
        HideButtons();
    }
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        upgradePanel.SetActive(true);

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].SetUpgradeButton(upgradeDatas[i]);
        }

        Time.timeScale = 0f;
    }
    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }
    public void Upgrade(int buttonId)
    {
        player.Upgrade(buttonId);
        ClosePanel();
    }
    public void ClosePanel()
    {
        HideButtons();

        Time.timeScale = 1f;
        upgradePanel.SetActive(false);
        player.AddExp(0);
    }
    void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
