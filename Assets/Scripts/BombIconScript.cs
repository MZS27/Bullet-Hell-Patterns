using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombIconScript : MonoBehaviour
{
    public PlayerScript player;
    public GameObject icon;
    // Start is called before the first frame update
    void Start()
    {
        icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isBombReady())
        {
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }
}
