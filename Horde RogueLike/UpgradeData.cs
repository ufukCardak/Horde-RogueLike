using UnityEngine;

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string upgradeExplanation;
    public Sprite upgradeIcon;
    public int upgradeNextValue;
    public int upgradeCurrentLevel;
    public int resetValue;
    public int mutation;
    public int resetMutation;
    private void OnEnable()
    {
        upgradeNextValue = resetValue;
        mutation = resetMutation;
        upgradeCurrentLevel = 0;
    }
}
