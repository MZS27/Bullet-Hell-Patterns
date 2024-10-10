using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Data", menuName = "Upgrade", order = 1)]
public class UpgradeSO : ScriptableObject
{
    public string upgradeTitle;
    public int cooldown;
    [TextArea(10, 10)] public string upgradeDescription;

    public enum upgrade
    {
        DashUpgrade, BarrierUpgrade, BombUpgrade  
    }
    [SerializeField] public upgrade upgradeName;
}
