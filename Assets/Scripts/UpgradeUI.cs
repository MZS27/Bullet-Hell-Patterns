using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeTitle;
    [SerializeField] TextMeshProUGUI upgradeDescription;
    [SerializeField] TextMeshProUGUI upgradeCooldown;

    public void RecieveUpgradeInfo(string title, string description, string cooldown)
    {
        upgradeTitle.text = title;
        upgradeDescription.text = description;
        upgradeCooldown.text = "Cooldown: " + cooldown + "s";
    }
}
