using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] UpgradeSO [] availableUpgrades = new UpgradeSO[3];
    [SerializeField] UpgradeUI[] upgradeButtons;

    public void ButtonUpgrade(UpgradeSO test)
    {
        RecieveSelectedUpgrade(test);
    }

    private void Start()
    {
        for (int i = 0; i < availableUpgrades.Length; i++)
        {
            upgradeButtons[i].RecieveUpgradeInfo(availableUpgrades[i].upgradeTitle, availableUpgrades[i].upgradeDescription, availableUpgrades[i].cooldown.ToString());
        }
    }

    void RecieveSelectedUpgrade (UpgradeSO recieved)
    {
        Debug.Log("" + recieved.upgradeName);
        switch (recieved.upgradeName)
        {
            case UpgradeSO.upgrade.BarrierUpgrade:
                UpgradeInfo.upgradeName = "barrier";
                break;

            case UpgradeSO.upgrade.BombUpgrade:
                UpgradeInfo.upgradeName = "bomb";
                break;

            case UpgradeSO.upgrade.DashUpgrade:
                UpgradeInfo.upgradeName = "dash";
                break;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

}
