using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("the recieved upgrade is: " + recieved);
                //unlock barrier for player
                //set barrier bool true for boss
                break;

            case UpgradeSO.upgrade.BombUpgrade:
                Debug.Log("the recieved upgrade is: " + recieved);
                break;

            case UpgradeSO.upgrade.DashUpgrade:
                break;

        }

    }

}
